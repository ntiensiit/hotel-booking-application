using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetRoomsByHotelId;

public record GetRoomsByHotelIdQueryV1(int HotelId) : IQuery<IEnumerable<Room<int>>>;