using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetHotelById;

public record GetHotelByIdQueryV1(int Id) : IQuery<object?>;