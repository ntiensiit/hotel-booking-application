using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetBookingByIdQueryV1(dynamic Id) : IQuery<object>;

public class GetBookingByIdQueryHandlerV1 : IQueryHandler<GetBookingByIdQueryV1, object>
{
    public async Task<object> HandleAsync(GetBookingByIdQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
