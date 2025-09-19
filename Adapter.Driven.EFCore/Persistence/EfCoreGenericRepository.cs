using Microsoft.EntityFrameworkCore;
using SharedKernel.SeedWork;
using System.Linq.Expressions;

namespace Adapter.Driven.EFCore.Persistence;

public class EfCoreGenericRepository<T, TId>(DbContext dbContext) : IGenericRepository<T, TId>
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

    public void Clear()
    {
        var allItems = DbContext.Set<T>().AsEnumerable();
        DbContext.Set<T>().RemoveRange(allItems);
    }

    public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AnyAsync(x => x.Id.Equals(item.Id), cancellationToken);
    }

    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(match, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<T>().Where(match).ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().LongCountAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }
}
