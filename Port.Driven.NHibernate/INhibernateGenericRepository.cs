using SharedKernel.SeedWork;
using System.Diagnostics.CodeAnalysis;

namespace Port.Driven.NHibernate;

public interface INHibernateGenericRepository<T, in TId> : IGenericRepository<T, TId>
    where T : class, IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    Task UpdateAsync([DisallowNull] T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync([DisallowNull] TId id, CancellationToken cancellationToken = default);
}
