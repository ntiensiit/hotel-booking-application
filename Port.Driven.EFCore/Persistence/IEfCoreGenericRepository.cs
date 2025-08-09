using SharedKernel.SeedWork;

namespace Port.Driven.EFCore.Persistence;

public interface IEfCoreGenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
}