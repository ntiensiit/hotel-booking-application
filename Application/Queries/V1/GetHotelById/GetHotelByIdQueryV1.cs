using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetHotelById;

public record GetHotelByIdQueryV1(dynamic Id) : IQuery<object>;