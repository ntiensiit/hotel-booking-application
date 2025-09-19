using Adapter.Driven.EFCore.Contexts;
using Adapter.Driving.AuthenticationServer.Services;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Settings;

namespace Adapter.Driving.AuthenticationServer.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddApplicationIdentity(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionStrings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        ArgumentException.ThrowIfNullOrEmpty(connectionStrings?.AuthenticationServerDBConnection);

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings.AuthenticationServerDBConnection));

        services.AddDataProtection();

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                var identitySettings = configuration.GetSection(IdentitySettings.SectionName).Get<IdentitySettings>();

                ArgumentNullException.ThrowIfNull(identitySettings);

                options.Password.RequireDigit = identitySettings.RequireDigit;
                options.Password.RequiredLength = identitySettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = identitySettings.RequireUppercase;
                options.Password.RequireLowercase = identitySettings.RequireLowercase;
                options.Password.RequiredUniqueChars = identitySettings.RequiredUniqueChars;
                options.User.RequireUniqueEmail = identitySettings.RequireUniqueEmail;
            })
            .AddRoles<ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddRoleValidator<RoleValidator<ApplicationRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<IdentitySettings>(configuration.GetSection(IdentitySettings.SectionName));

        services.AddSingleton(TimeProvider.System);
        services.AddHostedService<KeyRotationService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
