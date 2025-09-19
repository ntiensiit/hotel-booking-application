using Domain.Core.Entities;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetRoomsByHotelIdQueryV1(int HotelId) : IQuery<IEnumerable<Room>>;

public class GetRoomsByHotelIdQueryHandlerV1(IRoomRepository roomRepository) : IQueryHandler<GetRoomsByHotelIdQueryV1, IEnumerable<Room>>
{
    public async Task<IEnumerable<Room>> HandleAsync(GetRoomsByHotelIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await roomRepository.FindRoomsByHotelIdAsync(request.HotelId);
    }
}
