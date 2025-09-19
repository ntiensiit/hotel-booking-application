using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using SharedKernel.SeedWork;
using System.Linq.Expressions;

namespace Adapter.Driven.NHibernate.Persistence;

public class NHibernateGenericRepository<T, TId>(ISession session) : IGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    protected readonly ISession Session = session;

    public void Add(T item)
    {
        Session.Persist(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        Session.Persist(items);
    }

    public void Remove(T item)
    {
        Session.Delete(item);
    }

    public void RemoveRange(Expression<Func<T, bool>> match)
    {
        Session.Query<T>().Where(match).Delete();
    }

    public void Clear()
    {
        Session.CreateQuery($"delete from {typeof(T).Name}").ExecuteUpdate();
    }

    public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    {
        var count = await Session
            .QueryOver<T>()
            .Where(x => x.Id.Equals(item.Id))
            .Select(Projections.RowCount())
            .SingleOrDefaultAsync<int>(cancellationToken);
        return count > 0;
    }

    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default
    )
    {
        return await Session
            .QueryOver<T>()
            .Where(match)
            .Take(1)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default
    )
    {
        return await Session.QueryOver<T>().Where(match).ListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await Session
            .QueryOver<T>()
            .Select(Projections.RowCountInt64())
            .SingleOrDefaultAsync<long>(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await Session.GetAsync<T>(id, cancellationToken);
    }
}
