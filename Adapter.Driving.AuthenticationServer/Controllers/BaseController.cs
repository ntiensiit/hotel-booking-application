using Adapter.Driven.EFCore.Contexts;
using Adapter.Driving.AuthenticationServer.Services;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected class BaseControllerParameter(IServiceProvider serviceProvider)
    {
        private readonly ConcurrentDictionary<Type, object> _serviceCache = new();
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private TService Service<TService>() where TService : notnull
        {
            return (TService)_serviceCache.GetOrAdd(typeof(TService), type => _serviceProvider.GetRequiredService<TService>());
        }

        public UserManager<ApplicationUser> UserManager => Service<UserManager<ApplicationUser>>();
        public SignInManager<ApplicationUser> SignInManager => Service<SignInManager<ApplicationUser>>();
        public RoleManager<ApplicationRole> RoleManager => Service<RoleManager<ApplicationRole>>();
        public IJwtTokenService JwtTokenService => Service<IJwtTokenService>();
        public ApplicationDbContext ApplicationDbContext => Service<ApplicationDbContext>();
        public required ILogger Logger { get; init; }
    }

    protected BaseControllerParameter _parameter => new(HttpContext.RequestServices)
    {
        Logger = HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType())
    };
}
