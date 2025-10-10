using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Adapter.Driving.Web.Controllers;

public class BaseController : Controller
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
        public required ILogger Logger { get; init; }
    }

    protected BaseControllerParameter _parameter => new(HttpContext.RequestServices)
    {
        Logger = HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType())
    };
}
