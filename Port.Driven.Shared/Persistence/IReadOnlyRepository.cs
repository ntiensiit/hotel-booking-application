using SharedKernel.SeedWork;

namespace Port.Driven.Shared.Persistence;

public partial interface IReadOnlyRepository<T, in TId> : IRepository where TId : IEquatable<TId>
{
    long Count();
    bool ExistsById(TId id);
    IEnumerable<T> FindAll();
    IEnumerable<T> FindAllById(IEnumerable<TId> ids);
    T? FindById(TId id);
}

public partial interface IReadOnlyRepository<T, in TId>
{
    Task<long> CountAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAllByIdAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    Task<T?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);
}