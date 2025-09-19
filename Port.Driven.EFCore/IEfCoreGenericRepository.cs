using SharedKernel.SeedWork;

namespace Port.Driven.EFCore;

public interface IEfCoreGenericRepository<T, in TId> : IGenericRepository<T, TId>
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>;
