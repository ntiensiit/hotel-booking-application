using Adapter.Driven.EFCore.Contexts;
using Adapter.Driven.EFCore.Repositories;
using Adapter.Driven.MediatR;
using Adapter.Driven.NHibernate.Helpers;
using Adapter.Driven.NHibernate.Persistence;
using Adapter.Driven.NHibernate.Repositories;
using Application.Commands.CreateUserInfo;
using Application.Commands.CreateUserPrincipal;
using Application.Services;
using Domain.Core.Entities;
using Domain.Identity.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore.Repositories;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;
using Port.Driving.Shared.Services;
using SharedKernel.SeedWork;

namespace Adapter.Driving.ResourceServer.Extensions;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(CreateUserInfoCommandHandler).Assembly,
            typeof(CreateUserPrincipalCommandHandler).Assembly
        ));
        
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(CreateUserInfoCommandHandler).Assembly,
                typeof(CreateUserPrincipalCommandHandler).Assembly
            )

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
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateUserInfoCommand, UserInfo>, UserInfo>),
            typeof(MediatRCommandRequestHandler<CreateUserInfoCommand, UserInfo>));
        services.AddTransient(typeof(IRequestHandler<MediatRCommandRequest<CreateUserPrincipalCommand, UserPrincipal>, UserPrincipal>),
            typeof(MediatRCommandRequestHandler<CreateUserPrincipalCommand, UserPrincipal>));

        return services;
    }

    public static IServiceCollection AddNHibernate(this IServiceCollection services, IConfiguration configuration)
    {
        // Get connection string or throw
        var connectionString = configuration.GetConnectionString("ResourceServerDBConnection")
                               ?? throw new InvalidOperationException("No connection string found");
        
        NHibernateHelper.SetConnectionString(connectionString);
        
        // Register NHibernate session (singleton pattern)
        services.AddSingleton(_ => NHibernateHelper.OpenSession());

        // Register NHibernate repositories
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();

        // Register NHibernate unit of work
        services.AddScoped(typeof(IUnitOfWork<NHibernate.ISession>), typeof(UnitOfWork<NHibernate.ISession>));

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
        services.AddScoped(typeof(IGenericRepository<,>), typeof(Driven.EFCore.Persistence.GenericRepository<,>));
        services.AddScoped<IUserPrincipalRepository, UserPrincipalRepository>();
        services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        // Register EF Core unit of work
        services
            .AddScoped<IUnitOfWork<ApplicationDbContext>, Driven.EFCore.Persistence.UnitOfWork<ApplicationDbContext>>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}