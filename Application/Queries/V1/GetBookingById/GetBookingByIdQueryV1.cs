using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetBookingById;

public record GetBookingByIdQueryV1(dynamic Id) : IQuery<object>;