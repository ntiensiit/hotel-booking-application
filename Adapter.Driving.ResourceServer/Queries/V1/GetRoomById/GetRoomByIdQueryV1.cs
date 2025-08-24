using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetRoomById;

public record GetRoomByIdQueryV1(int Id) : IQuery<object?>;