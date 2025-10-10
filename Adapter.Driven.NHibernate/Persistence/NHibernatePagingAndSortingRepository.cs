using NHibernate;
using NHibernate.Criterion;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;
using System.Linq.Expressions;

namespace Adapter.Driven.NHibernate.Persistence;

public class NHibernatePagingAndSortingRepository<T, TId> : NHibernateGenericRepository<T, TId>, IPagingAndSortingRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    protected NHibernatePagingAndSortingRepository(ISession session) : base(session) { }

    public async Task<IPage<T>> FindAllAsync(IPageable pageable, Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageable);

        var query = Session.QueryOver<T>();
        query = ApplyFilter(query, match);

        using var statelessSession = Session.SessionFactory.OpenStatelessSession();
        var countQuery = statelessSession.QueryOver<T>();
        countQuery = ApplyFilter(countQuery, match);

        var totalCount = await countQuery
            .Select(Projections.RowCount())
            .SingleOrDefaultAsync<int>(cancellationToken);

        var results = await query
            .Skip(pageable.Offset)
            .Take(pageable.PageSize)
            .ListAsync(cancellationToken);

        return new Page<T>(results, totalCount, pageable);
    }
}
