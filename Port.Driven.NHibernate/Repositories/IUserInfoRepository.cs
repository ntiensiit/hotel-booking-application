using Domain.Core.Entities;

namespace Port.Driven.NHibernate.Repositories;

public interface IUserInfoRepository
{
    Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<UserInfo> Save(UserInfo userInfo, CancellationToken cancellationToken);
}