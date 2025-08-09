using Port.Driven.Shared.Persistence;

namespace Port.Driven.EFCore.Persistence;

public interface IEfCoreWriteOnlyRepository<T, TId> : IWriteOnlyRepository<T, TId> where TId : IEquatable<TId>
{
}