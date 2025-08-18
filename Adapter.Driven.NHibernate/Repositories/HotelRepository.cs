using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class HotelRepository : NHibernatePagingAndSortingRepository<Hotel<int>, int>, IHotelRepository
{
    public HotelRepository(ISession session) : base(session)
    {
    }
}