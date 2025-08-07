using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Application.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandler : IQueryHandler<GetCurrentUserInfoQuery, UserInfo>
{
    public async Task<UserInfo> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}