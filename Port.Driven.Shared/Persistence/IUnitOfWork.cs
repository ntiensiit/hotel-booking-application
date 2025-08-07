namespace Port.Driven.Shared.Persistence;

public partial interface IUnitOfWork<out TContext>
{
    TContext Context { get; }
}

public partial interface IUnitOfWork<out TContext>
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    int SaveChanges();
}

public partial interface IUnitOfWork<out TContext>
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}