using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class BookingRepository(ISession session) : NHibernatePagingAndSortingRepository<Booking, int>(session), IBookingRepository;
