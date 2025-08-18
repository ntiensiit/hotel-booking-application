using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IHotelRepository : IGenericRepository<Hotel<int>, int>, IPagingAndSortingRepository<Hotel<int>, int>
{
}