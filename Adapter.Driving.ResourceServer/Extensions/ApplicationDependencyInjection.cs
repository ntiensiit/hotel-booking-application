using Adapter.Driven.MediatR;
using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driven.NHibernate.Persistence;
using Adapter.Driven.NHibernate.Repositories;
using Application.Commands.V1.CreateCommands.CreateBooking;
using Application.Commands.V1.CreateCommands.CreateHotel;
using Application.Commands.V1.CreateCommands.CreateReview;
using Application.Commands.V1.CreateCommands.CreateRoom;
using Application.Commands.V1.CreateCommands.CreateService;
using Application.Commands.V1.CreateCommands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Shared.Settings;
using SharedKernel.SeedWork;
using ISession = NHibernate.ISession;

namespace Adapter.Driving.ResourceServer.Extensions;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(CreateUserCommandHandlerV1).Assembly,
            typeof(CreateBookingCommandHandlerV1).Assembly,
            typeof(CreateHotelCommandHandlerV1).Assembly,
            typeof(CreateRoomCommandHandlerV1).Assembly,
            typeof(CreateServiceCommandHandlerV1).Assembly,
            typeof(CreateReviewCommandHandlerV1).Assembly
        ));

        services.Scan(scan => scan
            .FromAssemblies(typeof(CreateUserCommandHandlerV1).Assembly, typeof(CreateBookingCommandHandlerV1).Assembly,
                typeof(CreateHotelCommandHandlerV1).Assembly, typeof(CreateRoomCommandHandlerV1).Assembly,
                typeof(CreateServiceCommandHandlerV1).Assembly, typeof(CreateReviewCommandHandlerV1).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        // Register application mediator
        services.AddScoped(typeof(IApplicationMediator), typeof(MediatRApplicationMediator));

        // Register command request handlers
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateUserCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateUserCommandV1, object>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateBookingCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateBookingCommandV1, object>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateHotelCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateHotelCommandV1, object>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateReviewCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateReviewCommandV1, object>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateRoomCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateRoomCommandV1, object>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateServiceCommandV1, object>, object>),
            typeof(MediatRCommandRequestHandler<CreateServiceCommandV1, object>));

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
        services.AddScoped(typeof(INhibernateUnitOfWork), typeof(NhibernateUnitOfWork));

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
                        var handler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                        };

                        using var httpClient = new HttpClient(handler);
                        try
                        {
                            var response = httpClient.GetStringAsync(jwtSettings.JWKS).GetAwaiter().GetResult();
                            return new JsonWebKeySet(response).Keys;
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
                    OnTokenValidated = ctx =>
                    {
                        Console.WriteLine("Token validated successfully!");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}