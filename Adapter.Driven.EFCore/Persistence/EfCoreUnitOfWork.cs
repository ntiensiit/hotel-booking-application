using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Port.Driven.EFCore.Persistence;

namespace Adapter.Driven.EFCore.Persistence;

public partial class EfCoreUnitOfWork : IEfCoreUnitOfWork, IDisposable
{
    private IDbContextTransaction? _currentTransaction;

    public EfCoreUnitOfWork(DbContext context)
    {
        Context = context;
    }

    private DbContext Context { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        var disposed = _currentTransaction == null;

        if (disposed || !disposing) return;

        // Đảm bảo transaction dispose
        _currentTransaction?.Dispose();
        _currentTransaction = null;

        //Dispose Context Object
        Context.Dispose();
    }
}

public partial class EfCoreUnitOfWork
{
    public void BeginTransaction()
    {
        _currentTransaction ??= Context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_currentTransaction == null) throw new InvalidOperationException("Transaction has not been started.");

        try
        {
            _currentTransaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        if (_currentTransaction == null) throw new InvalidOperationException("Transaction has not been started.");

        try
        {
            _currentTransaction.Rollback();
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public int SaveChanges()
    {
        return Context.SaveChanges();
    }
}

public partial class EfCoreUnitOfWork
{
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction ??= await Context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) throw new InvalidOperationException("Transaction has not been started.");

        try
        {
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) throw new InvalidOperationException("Transaction has not been started.");

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}