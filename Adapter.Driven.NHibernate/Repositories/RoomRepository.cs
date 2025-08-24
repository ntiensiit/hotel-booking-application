using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using NHibernate.Linq;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class RoomRepository : NHibernatePagingAndSortingRepository<Room<int>, int>, IRoomRepository
{
    public RoomRepository(ISession session) : base(session)
    {
    }

    public async Task<IEnumerable<Room<int>>> FindRoomsByHotelIdAsync(int hotelId)
    {
        return await Session.Query<Room<int>>().Where(r => r.HotelId.Equals(hotelId)).ToListAsync();
    }
}