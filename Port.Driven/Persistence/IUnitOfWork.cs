namespace Port.Driven.Persistence;

public interface IUnitOfWork<out TContext>
{
    TContext Context { get; }
    void CreateTransaction();
    void Commit();
    void Rollback();
    void Save();
}