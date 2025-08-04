using SharedKernel.SeedWork;

namespace Adapter.Persistence;

public interface IPagingAndSortingRepository<T, TId> : IRepository<T, TId>
{
}

public interface IListPagingAndSortingRepository
{
}