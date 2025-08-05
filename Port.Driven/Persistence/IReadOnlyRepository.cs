using SharedKernel.SeedWork;

namespace Port.Driven.Persistence;

public partial interface IReadOnlyRepository<T, TId> : IRepository<T, TId>
{
    long Count();
    bool ExistsById(TId id);
    IEnumerable<T> FindAll();
    IEnumerable<T> FindAllById(IEnumerable<TId> ids);
    T? FindById(TId id);
}

public partial interface IReadOnlyRepository<T, TId>
{
    Task<long> CountAsync();
    Task<bool> ExistsByIdAsync(TId id);
    Task<IEnumerable<T>> FindAllAsync();
    Task<IEnumerable<T>> FindAllByIdAsync(IEnumerable<TId> ids);
    Task<T?> FindByIdAsync(TId id);
}