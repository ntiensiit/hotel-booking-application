namespace SharedKernel.SeedWork;

public interface IRepository
{
}

public interface IRepository<T, TId> : IRepository
{
}

public interface IGenericRepository<T, TId> : IRepository<T, TId>
{
    void Add(T item);
    void AddRange(IEnumerable<T> items);
    void Remove(T item);
    void RemoveRange(Predicate<T> match);
    void Clear();
    bool Contains(T item);
    TId NextId();
    T Find(Predicate<T> match);
    IEnumerable<T> FindAll(Predicate<T> match);
    long Count();
}

public interface IEventStoreRepository<T, TId> : IRepository<T, TId>
    where T : IAggregateRoot<TId>, new()
{
    Task<T> LoadAsync(TId aggregateId);
    Task SaveAsync(T aggregate);
}