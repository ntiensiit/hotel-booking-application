namespace SharedKernel.SeedWork;

public interface IDomainRegistry
{
    T Get<T>(params object[] parameters) where T : class;
}

public interface IDomainServiceRegistry : IDomainRegistry
{
    T Get<T>() where T : notnull;
    object Get(Type type);
}

public interface IDomainObjectRegistry : IDomainRegistry
{
    T GetInstance<T>(params object[] parameters) where T : class;
    object GetInstance(Type type, params object[] parameters);
}