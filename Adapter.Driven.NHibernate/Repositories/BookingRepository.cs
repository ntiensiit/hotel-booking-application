using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class BookingRepository : NHibernatePagingAndSortingRepository<Booking<int>, int>, IBookingRepository
{
    public BookingRepository(ISession session) : base(session)
    {
    }
}