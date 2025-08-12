namespace SharedKernel.SeedWork;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<out TId> : IAggregateRoot, IEntity<TId> where TId : IEquatable<TId>
{
}