using SharedKernel.SeedWork;

namespace Port.Driven.Shared.Persistence;

public interface ICrudRepository<T, TId> : IReadOnlyRepository<T, TId>, IWriteOnlyRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>
{
}

public partial interface IListCrudRepository<T, TId> : ICrudRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>
{
    new IList<T> FindAll();
    new IList<T> FindAllById(IEnumerable<TId> ids);
    new IList<TDerived> SaveAll<TDerived>(IEnumerable<TDerived> items) where TDerived : T;
}

public partial interface IListCrudRepository<T, TId>
{
    new Task<IList<T>> FindAllAsync();
    new Task<IList<T>> FindAllByIdAsync(IEnumerable<TId> ids);
    new Task<IList<TDerived>> SaveAllAsync<TDerived>(IEnumerable<TDerived> items) where TDerived : T;
}