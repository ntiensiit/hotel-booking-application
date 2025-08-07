using SharedKernel.SeedWork;

namespace Port.Driven.Shared.Persistence;

public interface IPagingAndSortingRepository<T, TId> : IRepository<T, TId>
{
}

public interface IListPagingAndSortingRepository
{
}