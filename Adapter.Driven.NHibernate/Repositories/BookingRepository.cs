using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class BookingRepository : NhibernateGenericRepository<Booking<int>, int>, IBookingRepository
{
    public BookingRepository(ISession session) : base(session)
    {
    }
}