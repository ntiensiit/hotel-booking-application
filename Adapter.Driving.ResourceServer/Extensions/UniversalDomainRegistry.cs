using SharedKernel.SeedWork;

namespace Adapter.Driving.ResourceServer.Extensions;

public class UniversalDomainRegistry(IServiceProvider serviceProvider) : IDomainServiceRegistry, IDomainObjectRegistry
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public T GetInstance<T>(params object[] parameters) where T : class
    {
        return ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters);
    }

    public object GetInstance(Type type, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance(_serviceProvider, type, parameters);
    }

    public T Get<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public object Get(Type serviceType)
    {
        return _serviceProvider.GetRequiredService(serviceType);
    }

    T IDomainRegistry.Get<T>(params object[] parameters)
    {
        return GetInstance<T>(parameters);
    }
}
