using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Application.Queries.GetCurrentUserInfo;

public record GetCurrentUserInfoQuery : IQuery<UserInfo>;