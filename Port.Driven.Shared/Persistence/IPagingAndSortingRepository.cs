using SharedKernel.SeedWork;

namespace Port.Driven.Shared.Persistence;

public interface IPagingAndSortingRepository<T, TId> : IRepository
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    Task<IPage<T>> FindAllAsync(IPageable pageable);
    Task<IEnumerable<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>>? sort = null);
}