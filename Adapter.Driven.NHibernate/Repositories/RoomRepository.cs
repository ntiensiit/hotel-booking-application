using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class RoomRepository : NhibernateGenericRepository<Room, int>, IRoomRepository
{
    public RoomRepository(ISession session) : base(session)
    {
    }
}