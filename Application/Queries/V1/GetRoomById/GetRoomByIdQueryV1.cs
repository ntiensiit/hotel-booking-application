using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetRoomById;

public record GetRoomByIdQueryV1(dynamic Id) : IQuery<object>;