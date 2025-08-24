using Domain.Core.Entities;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetRoomsByHotelId;

public class GetRoomsByHotelIdQueryHandlerV1 : IQueryHandler<GetRoomsByHotelIdQueryV1, IEnumerable<Room<int>>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomsByHotelIdQueryHandlerV1(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<Room<int>>> Handle(GetRoomsByHotelIdQueryV1 request,
        CancellationToken cancellationToken)
    {
        return await _roomRepository.FindRoomsByHotelIdAsync(request.HotelId);
    }
}