using Port.Driven.Shared.Persistence;

namespace Port.Driven.EFCore.Persistence;

public interface IEfCoreReadOnlyRepository<T, TId> : IReadOnlyRepository<T, TId> where TId : IEquatable<TId>
{
}