using Adapter.Driven.MediatR;
using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driven.NHibernate.Persistence;
using Adapter.Driven.NHibernate.Repositories;
using Adapter.Driving.ResourceServer.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;
using Shared.Settings;
using SharedKernel.SeedWork;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Adapter.Driving.ResourceServer.Extensions;

public static class IServiceCollectionExtension
{
    private static readonly HttpClientHandler _httpClientHandler = new()
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
    };

    private static readonly HttpClient _httpClient = new(_httpClientHandler);

    public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
    {
        var appAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(appAssembly));
        services.AddScoped<IApplicationMediator, MediatRApplicationMediator>();
        services.Scan(scan =>
        {
            foreach (
                var handler in new[]
                {
                    typeof(ICommandHandler<>),
                    typeof(ICommandHandler<,>),
                    typeof(IQueryHandler<,>),
                }
            )
            {
                scan.FromAssemblies(appAssembly)
                    .AddClasses(c => c.AssignableTo(handler))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            }
        });

        var map = new Dictionary<Type, Func<Type, Type[], (Type request, Type handler, Type iface)>>
        {
            {
                typeof(IQuery<>),
                (implType, args) =>
                {
                    var resultType = args[0];
                    var request = typeof(MediatRQueryRequest<,>).MakeGenericType(
                        implType,
                        resultType
                    );
                    var handler = typeof(MediatRQueryRequestHandler<,>).MakeGenericType(
                        implType,
                        resultType
                    );
                    var iface = typeof(IRequestHandler<,>).MakeGenericType(request, resultType);
                    return (request, handler, iface);
                }
            },
            {
                typeof(ICommand<>),
                (implType, args) =>
                {
                    var resultType = args[0];
                    var request = typeof(MediatRCommandRequest<,>).MakeGenericType(
                        implType,
                        resultType
                    );
                    var handler = typeof(MediatRCommandRequestHandler<,>).MakeGenericType(
                        implType,
                        resultType
                    );
                    var iface = typeof(IRequestHandler<,>).MakeGenericType(request, resultType);
                    return (request, handler, iface);
                }
            },
            {
                typeof(ICommand),
                (implType, _) =>
                {
                    var request = typeof(MediatRCommandRequest<>).MakeGenericType(implType);
                    var handler = typeof(MediatRCommandRequestHandler<>).MakeGenericType(implType);
                    var iface = typeof(IRequestHandler<>).MakeGenericType(request);
                    return (request, handler, iface);
                }
            },
        };

        var allAppTypes = appAssembly
            .GetTypes()
            .AsParallel()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Impl = t,
                TargetInterface = t.GetInterfaces()
                    .FirstOrDefault(i =>
                        i switch
                        {
                            { IsGenericType: true } => map.ContainsKey(
                                i.GetGenericTypeDefinition()
                            ),
                            _ => map.ContainsKey(i),
                        }
                    ),
            })
            .Where(x => x.TargetInterface is not null);

        foreach (var x in allAppTypes)
        {
            var target = x.TargetInterface!;

            var key = target.IsGenericType ? target.GetGenericTypeDefinition() : target;
            var args = target.IsGenericType ? target.GetGenericArguments() : Type.EmptyTypes;

            var (_, handler, iface) = map[key](x.Impl, args);
            services.AddTransient(iface, handler);
        }

        return services;
    }

    public static IServiceCollection AddNHibernate(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Get connection string or throw
        var connectionStrings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        ArgumentException.ThrowIfNullOrEmpty(connectionStrings?.ResourceServerDBConnection);

        NHibernateHelper.SetConnectionString(connectionStrings.ResourceServerDBConnection);

        // Register NHibernate session
        services.AddScoped(_ => NHibernateHelper.OpenSession());

        // Register NHibernate repositories
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // Register NHibernate unit of work
        services.AddScoped(typeof(IUnitOfWork), typeof(NHibernateUnitOfWork));

        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<DatabaseSettings>(
            configuration.GetSection(DatabaseSettings.SectionName)
        );
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IDomainServiceRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainObjectRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainRegistry, UniversalDomainRegistry>();
        services.AddScoped<IHttpUserContextService, UserContextService>();

        return services;
    }

    public static IServiceCollection AddApplicationAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

                ArgumentNullException.ThrowIfNull(nameof(jwtSettings));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings?.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        Console.WriteLine("=== IssuerSigningKeyResolver called ===");
                        Console.WriteLine($"Raw token: {token}");
                        if (securityToken is JwtSecurityToken jwt)
                        {
                            Console.WriteLine(
                                $"JWT Header: {string.Join(", ", jwt.Header.Select(h => $"{h.Key}={h.Value}"))}"
                            );
                            Console.WriteLine(
                                $"JWT Payload: {string.Join(", ", jwt.Payload.Select(p => $"{p.Key}={p.Value}"))}"
                            );
                        }

                        Console.WriteLine($"KeyId (kid): {kid}");
                        Console.WriteLine($"Expected Issuer: {validationParameters.ValidIssuer}");
                        Console.WriteLine(
                            $"Expected Audience: {validationParameters.ValidAudience}"
                        );

                        try
                        {
                            var response = _httpClient
                                .GetStringAsync(jwtSettings?.JWKS)
                                .GetAwaiter()
                                .GetResult();
                            var jwks = new JsonWebKeySet(response);

                            var keys = jwks
                                .Keys.Where(k => string.IsNullOrEmpty(kid) || k.Kid == kid)
                                .ToList();
                            Console.WriteLine(
                                $"JWKS returned {jwks.Keys.Count} keys, matched {keys.Count} keys."
                            );
                            return keys;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to fetch JWKS: {ex.Message}");
                            return Enumerable.Empty<SecurityKey>();
                        }
                    },
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine($"Token invalid: {ctx.Exception}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        Console.WriteLine(
                            $"OnChallenge error: {ctx.Error}, desc: {ctx.ErrorDescription}"
                        );
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async ctx =>
                    {
                        Console.WriteLine("Token validated successfully!");
                        var userContextService =
                            ctx.HttpContext.RequestServices.GetRequiredService<IHttpUserContextService>();
                        await userContextService.AddAuthenticatedUserToContext(ctx);
                    },
                };
            });

        return services;
    }
}
