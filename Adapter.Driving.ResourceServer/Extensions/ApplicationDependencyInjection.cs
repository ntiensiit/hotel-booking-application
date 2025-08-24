using System.IdentityModel.Tokens.Jwt;
using Adapter.Driven.MediatR;
using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driven.NHibernate.Persistence;
using Adapter.Driven.NHibernate.Repositories;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateBooking;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateHotel;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateReview;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateRoom;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateService;
using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateUser;
using Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands.DeleteHotel;
using Adapter.Driving.ResourceServer.Helpers;
using Adapter.Driving.ResourceServer.Queries.V1.GetHotelById;
using Adapter.Driving.ResourceServer.Queries.V1.GetHotelsByPaging;
using Adapter.Driving.ResourceServer.Queries.V1.GetReviewsByHotelId;
using Adapter.Driving.ResourceServer.Queries.V1.GetRoomById;
using Adapter.Driving.ResourceServer.Queries.V1.GetRoomsByHotelId;
using Domain.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;
using Shared.Settings;
using SharedKernel.SeedWork;
using ISession = NHibernate.ISession;

namespace Adapter.Driving.ResourceServer.Extensions;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            // Command handlers
            typeof(CreateUserCommandHandlerV1).Assembly,
            typeof(CreateBookingCommandHandlerV1).Assembly,
            typeof(CreateHotelCommandHandlerV1).Assembly,
            typeof(CreateRoomCommandHandlerV1).Assembly,
            typeof(CreateServiceCommandHandlerV1).Assembly,
            typeof(CreateReviewCommandHandlerV1).Assembly,
            typeof(DeleteHotelByIdCommandHandlerV1).Assembly,
            // Query handlers
            typeof(GetHotelsByPagingQueryHandlerV1).Assembly,
            typeof(GetRoomByIdQueryHandlerV1).Assembly,
            typeof(GetRoomsByHotelIdQueryHandlerV1).Assembly,
            typeof(GetReviewsByHotelIdQueryHandlerV1).Assembly
        ));

        // Command handlers
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(CreateUserCommandHandlerV1).Assembly, typeof(CreateBookingCommandHandlerV1).Assembly,
                typeof(CreateHotelCommandHandlerV1).Assembly, typeof(CreateRoomCommandHandlerV1).Assembly,
                typeof(CreateServiceCommandHandlerV1).Assembly, typeof(CreateReviewCommandHandlerV1).Assembly,
                typeof(DeleteHotelByIdCommandHandlerV1).Assembly
            )
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        // Query handlers
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(GetHotelsByPagingQueryHandlerV1).Assembly,
                typeof(GetRoomByIdQueryHandlerV1).Assembly,
                typeof(GetRoomsByHotelIdQueryHandlerV1).Assembly,
                typeof(GetReviewsByHotelIdQueryHandlerV1).Assembly
            )
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        // Register application mediator
        services.AddScoped(typeof(IApplicationMediator), typeof(MediatRApplicationMediator));

        // Register command request handlers
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateUserCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateUserCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateBookingCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateBookingCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateHotelCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateHotelCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateReviewCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateReviewCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateRoomCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateRoomCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<CreateServiceCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateServiceCommandV1, object>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRCommandRequest<DeleteHotelByIdCommandV1>>),
            typeof(MediatRCommandRequestHandler<DeleteHotelByIdCommandV1>)
        );

        // Register query request handlers
        services.AddTransient(
            typeof(IRequestHandler<MediatRQueryRequest<GetHotelsByPagingQueryV1, IPage<Hotel<int>>>,
                IPage<Hotel<int>>>),
            typeof(MediatRQueryRequestHandler<GetHotelsByPagingQueryV1, IPage<Hotel<int>>>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRQueryRequest<GetHotelByIdQueryV1, object?>, object?>),
            typeof(MediatRQueryRequestHandler<GetHotelByIdQueryV1, object?>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRQueryRequest<GetRoomByIdQueryV1, object?>, object?>),
            typeof(MediatRQueryRequestHandler<GetRoomByIdQueryV1, object?>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRQueryRequest<GetRoomsByHotelIdQueryV1, IEnumerable<Room<int>>>,
                IEnumerable<Room<int>>>),
            typeof(MediatRQueryRequestHandler<GetRoomsByHotelIdQueryV1, IEnumerable<Room<int>>>)
        );
        services.AddTransient(
            typeof(IRequestHandler<MediatRQueryRequest<GetReviewsByHotelIdQueryV1, IEnumerable<Review<int>>>,
                IEnumerable<Review<int>>>),
            typeof(MediatRQueryRequestHandler<GetReviewsByHotelIdQueryV1, IEnumerable<Review<int>>>)
        );

        return services;
    }

    public static IServiceCollection AddNHibernate(this IServiceCollection services, IConfiguration configuration)
    {
        // Get connection string or throw
        var connectionStrings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();
        if (string.IsNullOrEmpty(connectionStrings?.ResourceServerDBConnection))
            throw new InvalidOperationException($"{nameof(connectionStrings.ResourceServerDBConnection)} is required.");

        NHibernateHelper.SetConnectionString(connectionStrings.ResourceServerDBConnection);

        // Register NHibernate session (singleton pattern)
        services.AddScoped<ISession>(_ => NHibernateHelper.OpenSession());

        // Register NHibernate repositories
        services.AddScoped(typeof(INhibernateGenericRepository<,>), typeof(NhibernateGenericRepository<,>));
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        // Register NHibernate unit of work
        services.AddScoped(typeof(IUnitOfWork), typeof(NhibernateUnitOfWork));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IDomainServiceRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainObjectRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainRegistry, UniversalDomainRegistry>();
        services.AddScoped<IUserContextService, UserContextService>();

        return services;
    }

    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        Console.WriteLine("=== IssuerSigningKeyResolver called ===");
                        Console.WriteLine($"Raw token: {token}");
                        if (securityToken is JwtSecurityToken jwt)
                        {
                            Console.WriteLine(
                                $"JWT Header: {string.Join(", ", jwt.Header.Select(h => h.Key + "=" + h.Value))}");
                            Console.WriteLine(
                                $"JWT Payload: {string.Join(", ", jwt.Payload.Select(p => p.Key + "=" + p.Value))}");
                        }

                        Console.WriteLine($"KeyId (kid): {kid}");
                        Console.WriteLine($"Expected Issuer: {validationParameters.ValidIssuer}");
                        Console.WriteLine($"Expected Audience: {validationParameters.ValidAudience}");

                        var handler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                        };

                        using var httpClient = new HttpClient(handler);
                        try
                        {
                            var response = httpClient.GetStringAsync(jwtSettings.JWKS).GetAwaiter().GetResult();
                            var jwks = new JsonWebKeySet(response);

                            var keys = string.IsNullOrEmpty(kid)
                                ? jwks.Keys
                                : jwks.Keys.Where(k => k.Kid == kid).ToList();

                            Console.WriteLine($"JWKS returned {jwks.Keys.Count} keys, matched {keys.Count()} keys");
                            return keys;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to fetch JWKS: {ex.Message}");
                            return null;
                        }
                    }
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
                        Console.WriteLine($"OnChallenge error: {ctx.Error}, desc: {ctx.ErrorDescription}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async ctx =>
                    {
                        Console.WriteLine("Token validated successfully!");
                        var userContextService =
                            ctx.HttpContext.RequestServices.GetRequiredService<IUserContextService>();
                        await userContextService.AddAuthenticatedUserToContext(ctx);
                    }
                };
            });

        return services;
    }
}