using SharedKernel.SeedWork;

namespace Port.Driven.Shared.Persistence;

public partial interface IWriteOnlyRepository<T, TId> : IRepository<T, TId> where TId : IEquatable<TId>
{
    void Delete(T item);
    void DeleteAll();
    void DeleteAll(IEnumerable<T> items);
    void DeleteAllById(IEnumerable<TId> ids);
    void DeleteById(TId id);

    void Save<TDerived>(TDerived item) where TDerived : class, T;
    void SaveAll<TDerived>(IEnumerable<TDerived> items) where TDerived : class, T;
}

public partial interface IWriteOnlyRepository<T, TId>
{
    Task DeleteAsync(T item, CancellationToken cancellationToken = default);
    Task DeleteAllAsync(CancellationToken cancellationToken);
    Task DeleteAllAsync(IEnumerable<T> items, CancellationToken cancellationToken = default);
    Task DeleteAllByIdAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task SaveAsync<TDerived>(TDerived item, CancellationToken cancellationToken = default) where TDerived : class, T;

    Task SaveAllAsync<TDerived>(IEnumerable<TDerived> items, CancellationToken cancellationToken = default)
        where TDerived : class, T;
}