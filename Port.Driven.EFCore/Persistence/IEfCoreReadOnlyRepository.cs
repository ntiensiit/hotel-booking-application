using Port.Driven.Shared.Persistence;

namespace Port.Driven.EFCore.Persistence;

public interface IEfCoreReadOnlyRepository<T, in TId> : IReadOnlyRepository<T, TId> where TId : IEquatable<TId>
{
}