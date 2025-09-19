using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class RoomRepository(ISession session) : NHibernatePagingAndSortingRepository<Room, int>(session), IRoomRepository
{
    public async Task<IEnumerable<Room>> FindRoomsByHotelIdAsync(int hotelId)
    {
        return await Session.QueryOver<Room>().Where(r => r.HotelId == hotelId).ListAsync();
    }
}
