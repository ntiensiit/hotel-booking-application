using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore;
using SharedKernel.SeedWork;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Adapter.Driven.EFCore.Persistence;

public partial class EfCoreGenericRepository<T, TId>(DbContext dbContext) : IEfCoreGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    protected readonly DbContext DbContext = dbContext;

    public void Add(T item)
    {
        DbContext.Set<T>().Add(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        DbContext.Set<T>().AddRange(items);
    }

    public void Remove(T item)
    {
        DbContext.Set<T>().Remove(item);
    }

    public void RemoveRange(Expression<Func<T, bool>> match)
    {
        var itemsToRemove = DbContext.Set<T>().Where(match).ToList();
        DbContext.Set<T>().RemoveRange(itemsToRemove);
    }

    //public void Clear()
    //{
    //    var allItems = DbContext.Set<T>().AsEnumerable();
    //    DbContext.Set<T>().RemoveRange(allItems);
    //}

    //public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    //{
    //    return await DbContext.Set<T>().AnyAsync(x => x.Id.Equals(item.Id), cancellationToken);
    //}

    public async Task<T?> FindAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(match, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default)
    {
        var query = DbContext.Set<T>().AsQueryable();

        if (match != null)
            query = query.Where(match);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default)
    {
        var query = DbContext.Set<T>().AsQueryable();

        if (match != null)
            query = query.Where(match);

        return await query.LongCountAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AnyAsync(match, cancellationToken);
    }

    public Task<T?> FindFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FindLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFirstAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveLastAsync(Expression<Func<T, object>> orderBy = null!, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public T? FindFirst(Expression<Func<T, object>> orderBy = null!)
    {
        throw new NotImplementedException();
    }

    public T? FindLast(Expression<Func<T, object>> orderBy = null!)
    {
        throw new NotImplementedException();
    }

    public void RemoveFirst(Expression<Func<T, object>> orderBy = null!)
    {
        throw new NotImplementedException();
    }

    public void RemoveLast(Expression<Func<T, object>> orderBy = null!)
    {
        throw new NotImplementedException();
    }
}

public partial class EfCoreGenericRepository<T, TId>
{
    public T? Find([DisallowNull] Expression<Func<T, bool>> match)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> match = null!)
    {
        throw new NotImplementedException();
    }

    public long Count(Expression<Func<T, bool>> match = null!)
    {
        throw new NotImplementedException();
    }

    public T? FindById(TId id)
    {
        throw new NotImplementedException();
    }

    public bool Exist([DisallowNull] Expression<Func<T, bool>> match)
    {
        throw new NotImplementedException();
    }
}