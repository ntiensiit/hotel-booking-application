using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetBookingById;

public class GetBookingByIdQueryHandlerV1 : IQueryHandler<GetBookingByIdQueryV1, object>
{
    public async Task<object> Handle(GetBookingByIdQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}