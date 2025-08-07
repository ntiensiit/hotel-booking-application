using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using SharedKernel.SeedWork;

namespace Adapter.Driven.NHibernate.Persistence;

public class GenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    private readonly ISession _session;

    public GenericRepository(ISession session)
    {
        _session = session;
    }

    public void Add(T item)
    {
        _session.Persist(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        // foreach (var item in items) _session.Persist(item);
    }

    public void Remove(T item)
    {
        _session.Delete(item);
    }

    public void RemoveRange(Expression<Func<T, bool>> match)
    {
        // var itemsToDelete = _session.Query<T>().Where(match).ToList();
        // foreach (var item in itemsToDelete) _session.Delete(item);
    }

    public void Clear()
    {
        _session.CreateQuery($"delete from {typeof(T).Name}").ExecuteUpdate();
    }

    public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>().AnyAsync(e => e.Id.Equals(item.Id), cancellationToken);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>().FirstOrDefaultAsync(match, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>().Where(match).ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _session.Query<T>().LongCountAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _session.GetAsync<T>(id, cancellationToken);
    }
}