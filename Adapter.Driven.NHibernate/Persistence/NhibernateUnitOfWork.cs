using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Persistence;

public partial class NHibernateUnitOfWork(ISession context) : INHibernateUnitOfWork, IDisposable
{
    private ITransaction? _currentTransaction;

    private ISession Context { get; } = context;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        var disposed = _currentTransaction == null;

        if (disposed || !disposing)
            return;

        _currentTransaction?.Dispose();
        _currentTransaction = null;
    }
}

public partial class NHibernateUnitOfWork
{
    public void BeginTransaction()
    {
        _currentTransaction ??= Context.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_currentTransaction is not { IsActive: true })
            throw new InvalidOperationException("Transaction has not been started.");

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
        if (_currentTransaction is not { IsActive: true })
            throw new InvalidOperationException("Transaction has not been started.");

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
        Context.Flush();
        return 0;
    }
}

public partial class NHibernateUnitOfWork
{
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction ??= Context.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not { IsActive: true })
            throw new InvalidOperationException("Transaction has not been started.");

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
        if (_currentTransaction is not { IsActive: true })
            throw new InvalidOperationException("Transaction has not been started.");

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
        await Context.FlushAsync(cancellationToken);
        return 0;
    }
}
