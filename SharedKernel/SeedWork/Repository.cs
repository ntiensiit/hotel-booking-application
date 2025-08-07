using System.Linq.Expressions;

namespace SharedKernel.SeedWork;

public interface IRepository<T, TId>
{
}

public interface IGenericRepository<T, TId> : IRepository<T, TId>
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    void Add(T item);
    void AddRange(IEnumerable<T> items);
    void Remove(T item);
    void RemoveRange(Expression<Func<T, bool>> match);
    void Clear();
    Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    Task<long> CountAsync(CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    // TId NextId();
}

public interface IEventStoreRepository<T, TId> : IRepository<T, TId>
    where T : IDomainEvent, new()
    where TId : IEquatable<TId>
{
    Task<T> LoadAsync(TId aggregateId, CancellationToken cancellationToken = default);
    Task SaveAsync(T domainEvent, CancellationToken cancellationToken = default);
}