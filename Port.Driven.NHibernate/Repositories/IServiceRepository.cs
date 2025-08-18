using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IServiceRepository : IGenericRepository<Service<int>, int>,
    IPagingAndSortingRepository<Service<int>, int>
{
}