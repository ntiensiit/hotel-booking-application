using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Port.Driven.NHibernate;
using SharedKernel.SeedWork;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Adapter.Driven.NHibernate.Persistence;

public partial class NHibernateGenericRepository<T, TId>(ISession session) : INHibernateGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    internal readonly ISession Session = session;

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

    //public void Clear()
    //{
    //    Session.CreateQuery($"delete from {typeof(T).Name}").ExecuteUpdate();
    //}

    //public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    //{
    //    T alias = default!;

    //    var count = await Session
    //        .QueryOver(() => alias)
    //        .Where(Restrictions.Eq(Projections.Property(() => alias.Id), item.Id))
    //        .Select(Projections.RowCount())
    //        .SingleOrDefaultAsync<int>(cancellationToken);

    //    return count > 0;
    //}

    public async Task<T?> FindAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        var query = Session.QueryOver<T>();

        return await query
           .Where(match)
           .Take(1)
           .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default)
    {
        var query = Session.QueryOver<T>();

        query = ApplyFilter(query, match);

        return await query.Future().GetEnumerableAsync(cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default)
    {
        var query = Session.QueryOver<T>();

        query = ApplyFilter(query, match);

        var count = await query
            .Select(Projections.RowCountInt64())
            .SingleOrDefaultAsync<long>(cancellationToken);

        return count;
    }

    public async Task<T?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await Session.GetAsync<T>(id, cancellationToken);
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        return await CountAsync(match, cancellationToken) > 0;
    }
}

public partial class NHibernateGenericRepository<T, TId>
{
    public T? Find([DisallowNull] Expression<Func<T, bool>> match)
    {
        return Session.QueryOver<T>().Where(match).Take(1).SingleOrDefault();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> match = null!)
    {
        var query = Session.QueryOver<T>();

        query = ApplyFilter(query, match);

        return query.Future();
    }

    public long Count(Expression<Func<T, bool>> match = null!)
    {
        var query = Session.QueryOver<T>();

        query = ApplyFilter(query, match);

        return query.RowCountInt64();
    }

    public T? FindById(TId id)
    {
        return Session.Get<T>(id);
    }

    public bool Exist([DisallowNull] Expression<Func<T, bool>> match)
    {
        return Count() > 0;
    }
}

public partial class NHibernateGenericRepository<T, TId>
{
    public async Task<T?> FindFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        var query = Session.QueryOver<T>();

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy).Asc;
        }
        else
        {
            query = query.OrderBy(Projections.Property<T>(x => x.Id)).Asc;
        }

        return await query.Take(1).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> FindLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        var query = Session.QueryOver<T>();

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy).Desc;
        }
        else
        {
            query = query.OrderBy(Projections.Property<T>(x => x.Id)).Desc;
        }

        return await query.Take(1).SingleOrDefaultAsync(cancellationToken);
    }

    public T? FindFirst(Expression<Func<T, object>> orderBy = null!)
    {
        var query = Session.QueryOver<T>();

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy).Asc;
        }
        else
        {
            query = query.OrderBy(Projections.Property<T>(x => x.Id)).Asc;
        }

        return query.Take(1).SingleOrDefault();
    }

    public T? FindLast(Expression<Func<T, object>> orderBy = null!)
    {
        var query = Session.QueryOver<T>();

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy).Desc;
        }
        else
        {
            query = query.OrderBy(Projections.Property<T>(x => x.Id)).Desc;
        }

        return query.Take(1).SingleOrDefault();
    }

    public void RemoveFirst(Expression<Func<T, object>> orderBy = null!)
    {
        Session.Delete(FindFirst(orderBy));
    }

    public void RemoveLast(Expression<Func<T, object>> orderBy = null!)
    {
        Session.Delete(FindLast(orderBy));
    }

    public async Task RemoveFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        await Session.DeleteAsync(FindFirstAsync(orderBy, cancellationToken), cancellationToken);
    }

    public async Task RemoveLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        await Session.DeleteAsync(FindLastAsync(orderBy, cancellationToken), cancellationToken);
    }
}

public partial class NHibernateGenericRepository<T, TId>
{
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Session.UpdateAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
    {
        if (!ids.Any())
            return [];

        return await Session.QueryOver<T>()
            .WhereRestrictionOn(x => x.Id).IsIn(ids.ToArray())
            .ListAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await Session.LoadAsync<T>(id, cancellationToken);
        await Session.DeleteAsync(entity, cancellationToken);
    }

    private protected static IQueryOver<T, T> ApplyFilter(IQueryOver<T, T> query, Expression<Func<T, bool>> match = null!)
    {
        if (match != null)
            query = query.Where(match);
        return query;
    }
}