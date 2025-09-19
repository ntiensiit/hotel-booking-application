using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate;

public interface INHibernateGenericRepository<T, in TId> : IGenericRepository<T, TId>
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>;
