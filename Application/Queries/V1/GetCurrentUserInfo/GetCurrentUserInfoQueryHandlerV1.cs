using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandlerV1 : IQueryHandler<GetCurrentUserInfoQueryV1, object>
{
    public async Task<object> Handle(GetCurrentUserInfoQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}