using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class HotelRepository(ISession session) : NHibernatePagingAndSortingRepository<Hotel, int>(session), IHotelRepository;
