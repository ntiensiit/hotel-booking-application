using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using System.Collections.Concurrent;

namespace Adapter.Driving.ResourceServer.Controllers;

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

        public IApplicationMediator ApplicationMediator => Service<IApplicationMediator>();
        public required ILogger Logger { get; init; }
    }

    protected BaseControllerParameter _parameter => new(HttpContext.RequestServices)
    {
        Logger = HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType())
    };
}
