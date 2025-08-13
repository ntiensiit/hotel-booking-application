using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandlerV1 : IQueryHandler<GetCurrentUserInfoQueryV1, UserInfo>
{
    public async Task<UserInfo> Handle(GetCurrentUserInfoQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}