namespace SharedKernel.SeedWork;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<TId> : IAggregateRoot
{
    TId Id { get; protected set; }
}