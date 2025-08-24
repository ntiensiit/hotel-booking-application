using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IRoomRepository : IGenericRepository<Room<int>, int>, IPagingAndSortingRepository<Room<int>, int>
{
    Task<IEnumerable<Room<int>>> FindRoomsByHotelIdAsync(int hotelId);
}