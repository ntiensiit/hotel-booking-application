using NHibernate;
using NHibernate.Linq;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Adapter.Driven.NHibernate.Persistence;

public class NHibernatePagingAndSortingRepository<T, TId> : NhibernateGenericRepository<T, TId>,
    IPagingAndSortingRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    protected NHibernatePagingAndSortingRepository(ISession session) : base(session)
    {
    }

    public async Task<IPage<T>> FindAllAsync(IPageable pageable)
    {
        if (pageable == null) throw new ArgumentNullException(nameof(pageable));

        var query = Session.Query<T>();

        var totalCount = await query.CountAsync();

        var results = await query
            .Skip(pageable.Offset)
            .Take(pageable.PageSize)
            .ToListAsync();

        return new Page<T>(results, totalCount, pageable);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Func<IQueryable<T>, IQueryable<T>>? sort = null)
    {
        var query = Session.Query<T>();

        if (sort != null) query = sort(query);

        return await query.ToListAsync();
    }
}