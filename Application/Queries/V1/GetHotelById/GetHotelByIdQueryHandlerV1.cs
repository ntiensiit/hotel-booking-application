using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetHotelById;

public class GetHotelByIdQueryHandlerV1 : IQueryHandler<GetHotelByIdQueryV1, object>
{
    public async Task<object> Handle(GetHotelByIdQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}