using NHibernate;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Adapter.Driven.NHibernate.Persistence;

public class NHibernatePagingAndSortingRepository<T, TId> : NHibernateGenericRepository<T, TId>, IPagingAndSortingRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    protected NHibernatePagingAndSortingRepository(ISession session) : base(session) { }

    public async Task<IPage<T>> FindAllAsync(IPageable pageable)
    {
        ArgumentNullException.ThrowIfNull(pageable);

        var totalCount = await Session.QueryOver<T>().RowCountAsync();

        var results = await Session
            .QueryOver<T>()
            .Skip(pageable.Offset)
            .Take(pageable.PageSize)
            .ListAsync();

        return new Page<T>(results, totalCount, pageable);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>>? sort = null)
    {
        var results = await Session.QueryOver<T>().ListAsync();

        if (sort != null)
        {
            var sorted = sort(results.AsQueryable());
            return [.. sorted];
        }

        return results;
    }
}
