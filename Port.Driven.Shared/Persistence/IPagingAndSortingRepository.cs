using SharedKernel.SeedWork;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Port.Driven.Shared.Persistence;

public interface IPagingAndSortingRepository<T, TId> : IRepository
    where T : IEntity<TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    Task<IPage<T>> FindAllAsync([DisallowNull] IPageable pageable, Expression<Func<T, bool>> match = null!, CancellationToken cancellationToken = default);
}
