using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Persistence;

public interface INhibernateGenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
}