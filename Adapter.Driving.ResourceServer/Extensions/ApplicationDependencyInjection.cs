using Adapter.Driven.EFCore.Contexts;
using Adapter.Driven.EFCore.Persistence;
using Adapter.Driven.EFCore.Repositories;
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
using Domain.Core.Repositories;
using Domain.Identity.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore.Persistence;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.Shared.Events;
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
        var connectionString = configuration.GetConnectionString("ResourceServerDBConnection")
                               ?? throw new InvalidOperationException("No connection string found");

        NHibernateHelper.SetConnectionString(connectionString);

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

    public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        // Get connection string or throw
        var connectionString = configuration.GetConnectionString("AuthenticationServerDBConnection")
                               ?? throw new InvalidOperationException("No connection string found");

        // Register EF Core DbContext for Identity
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        // Register EF Core repositories
        services.AddScoped(typeof(IEfCoreGenericRepository<,>), typeof(EfCoreGenericRepository<,>));
        services.AddScoped<IUserPrincipalRepository, UserPrincipalRepository>();
        services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        // Register EF Core unit of work
        services.AddScoped(typeof(IEfCoreUnitOfWork), typeof(EfCoreUnitOfWork));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDomainServiceRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainObjectRegistry, UniversalDomainRegistry>();
        services.AddSingleton<IDomainRegistry, UniversalDomainRegistry>();

        return services;
    }
}