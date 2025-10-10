namespace SharedKernel.SeedWork;

public interface IAggregateRoot;

public interface IAggregateRoot<out TId> : IAggregateRoot
    where TId : IEquatable<TId>, IComparable<TId>;
