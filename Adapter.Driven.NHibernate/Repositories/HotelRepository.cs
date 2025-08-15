using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class HotelRepository : NhibernateGenericRepository<Hotel<int>, int>, IHotelRepository
{
    public HotelRepository(ISession session) : base(session)
    {
    }
}