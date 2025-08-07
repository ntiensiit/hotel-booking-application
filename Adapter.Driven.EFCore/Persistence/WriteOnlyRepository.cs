using Adapter.Driven.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Adapter.Driven.EFCore.Persistence;

public partial class WriteOnlyRepository<T, TId> : IWriteOnlyRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>
{
    private readonly DbContext _dbContext;

    public WriteOnlyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(T item)
    {
        _dbContext.Set<T>().Remove(item);
    }

    public void DeleteAll()
    {
        _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
    }

    public void DeleteAll(IEnumerable<T> items)
    {
        _dbContext.Set<T>().RemoveRange(items);
    }

    public void DeleteAllById(IEnumerable<TId> ids)
    {
        var entitiesToDelete = _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToList();
        _dbContext.Set<T>().RemoveRange(entitiesToDelete);
    }

    public void DeleteById(TId id)
    {
        var entityToDelete = _dbContext.Set<T>().Find(id);
        if (entityToDelete != null) _dbContext.Set<T>().Remove(entityToDelete);
    }

    public void Save<TDerived>(TDerived item) where TDerived : class, T
    {
        _dbContext.Set<TDerived>().Update(item);
    }

    public void SaveAll<TDerived>(IEnumerable<TDerived> items) where TDerived : class, T
    {
        foreach (var item in items) _dbContext.Set<TDerived>().Update(item);
    }
}

public partial class WriteOnlyRepository<T, TId>
{
    public async Task DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(item);
        await Task.CompletedTask;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteAllAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(items);
        await Task.CompletedTask;
    }

    public async Task DeleteAllByIdAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
    {
        var entitiesToDelete = await _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        _dbContext.Set<T>().RemoveRange(entitiesToDelete);
        await Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await _dbContext.Set<T>().FindAsync(new object?[] { id }, cancellationToken);
        if (entityToDelete != null) _dbContext.Set<T>().Remove(entityToDelete);
        await Task.CompletedTask;
    }

    public async Task SaveAsync<TDerived>(TDerived item, CancellationToken cancellationToken = default)
        where TDerived : class, T
    {
        _dbContext.Set<TDerived>().Update(item);
        await Task.CompletedTask;
    }

    public async Task SaveAllAsync<TDerived>(IEnumerable<TDerived> items, CancellationToken cancellationToken = default)
        where TDerived : class, T
    {
        foreach (var item in items) _dbContext.Set<TDerived>().Update(item);
        await Task.CompletedTask;
    }
}