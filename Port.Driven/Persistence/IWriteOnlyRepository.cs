namespace Port.Driven.Persistence;

public partial interface IWriteOnlyRepository<T, TId> : IRepository<T, TId>
{
    void Delete(T item);
    void DeleteAll();
    void DeleteAll(IEnumerable<T> items);
    void DeleteAllById(IEnumerable<TId> ids);
    void DeleteById(TId id);

    void Save<TDerived>(TDerived item) where TDerived : T;
    void SaveAll<TDerived>(IEnumerable<TDerived> items) where TDerived : T;
}

public partial interface IWriteOnlyRepository<T, TId>
{
    Task DeleteAsync(T item);
    Task DeleteAllAsync();
    Task DeleteAllAsync(IEnumerable<T> items);
    Task DeleteAllByIdAsync(IEnumerable<TId> ids);
    Task DeleteByIdAsync(TId id);

    Task SaveAsync<TDerived>(TDerived item) where TDerived : T;
    Task SaveAllAsync<TDerived>(IEnumerable<TDerived> items) where TDerived : T;
}