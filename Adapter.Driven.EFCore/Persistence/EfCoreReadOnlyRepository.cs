using Adapter.Driven.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore.Persistence;
using SharedKernel.SeedWork;

namespace Adapter.Driven.EFCore.Persistence;

public partial class EfCoreReadOnlyRepository<T, TId> : IEfCoreReadOnlyRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    private readonly DbContext _dbContext;

    public EfCoreReadOnlyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public long Count()
    {
        return _dbContext.Set<T>().LongCount();
    }

    public bool ExistsById(TId id)
    {
        return _dbContext.Set<T>().Any(e => e.Id.Equals(id));
    }

    public IEnumerable<T> FindAll()
    {
        return _dbContext.Set<T>().ToList();
    }

    public IEnumerable<T> FindAllById(IEnumerable<TId> ids)
    {
        return _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToList();
    }

    public T? FindById(TId id)
    {
        return _dbContext.Set<T>().Find(id);
    }
}

public partial class EfCoreReadOnlyRepository<T, TId>
{
    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().LongCountAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AnyAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllByIdAsync(IEnumerable<TId> ids,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
    }

    public async Task<T?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }
}