using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IBookingRepository : IGenericRepository<Booking<int>, int>,
    IPagingAndSortingRepository<Booking<int>, int>
{
}