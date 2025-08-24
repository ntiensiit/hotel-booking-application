using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetBookingById;

public record GetBookingByIdQueryV1(dynamic Id) : IQuery<object>;