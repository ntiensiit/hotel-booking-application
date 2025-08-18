using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class RoomRepository : NHibernatePagingAndSortingRepository<Room<int>, int>, IRoomRepository
{
    public RoomRepository(ISession session) : base(session)
    {
    }
}