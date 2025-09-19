using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetCurrentUserInfoQueryV1 : IQuery<object>;

public class GetCurrentUserInfoQueryHandlerV1 : IQueryHandler<GetCurrentUserInfoQueryV1, object>
{
    public async Task<object> HandleAsync(GetCurrentUserInfoQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
