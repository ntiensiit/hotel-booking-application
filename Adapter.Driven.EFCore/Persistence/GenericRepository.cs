using System.Linq.Expressions;
using Adapter.Driven.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.SeedWork;

namespace Adapter.Driven.EFCore.Persistence;

public class GenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    private readonly DbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T item)
    {
        _dbContext.Set<T>().Add(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        _dbContext.Set<T>().AddRange(items);
    }

    public void Remove(T item)
    {
        _dbContext.Set<T>().Remove(item);
    }

    public void RemoveRange(Expression<Func<T, bool>> match)
    {
        var itemsToRemove = _dbContext.Set<T>().Where(match).ToList();
        _dbContext.Set<T>().RemoveRange(itemsToRemove);
    }

    public void Clear()
    {
        var allItems = _dbContext.Set<T>().AsEnumerable();
        _dbContext.Set<T>().RemoveRange(allItems);
    }

    public async Task<bool> ContainsAsync(T item, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AnyAsync(x => x.Id.Equals(item.Id), cancellationToken);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(match, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Where(match).ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().LongCountAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }
}