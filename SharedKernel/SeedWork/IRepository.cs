using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SharedKernel.SeedWork;

public interface IRepository;

public partial interface IGenericRepository<T, in TId> : IRepository
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    void Add(T item);
    void AddRange(IEnumerable<T> items);
    void Remove(T item);
    void RemoveRange(Expression<Func<T, bool>> match);
    //void Clear();
    //Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default);
    Task<T?> FindAsync([DisallowNull] Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default);
    Task<T?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync([DisallowNull] Expression<Func<T, bool>> match, CancellationToken cancellationToken = default);
    Task<T?> FindFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default);
    Task<T?> FindLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default);
    Task RemoveFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default);
    Task RemoveLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default);
}

public partial interface IGenericRepository<T, in TId>
{
    T? Find([DisallowNull] Expression<Func<T, bool>> match);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> match = null!);
    long Count(Expression<Func<T, bool>> match = null!);
    T? FindById(TId id);
    bool Exist([DisallowNull] Expression<Func<T, bool>> match);
    T? FindFirst(Expression<Func<T, object>> orderBy = null!);
    T? FindLast(Expression<Func<T, object>> orderBy = null!);
    void RemoveFirst(Expression<Func<T, object>> orderBy = null!);
    void RemoveLast(Expression<Func<T, object>> orderBy = null!);
}

public interface IEventStoreRepository<T, in TId> : IRepository
    where T : IDomainEvent, new()
    where TId : IEquatable<TId>, IComparable<TId>
{
    Task<T> LoadAsync(TId aggregateId, CancellationToken cancellationToken = default);
    Task SaveAsync(T domainEvent, CancellationToken cancellationToken = default);
}
