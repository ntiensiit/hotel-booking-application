using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class UserInfoRepository : IUserInfoRepository
{
    private readonly ISession _session;

    public UserInfoRepository(ISession session)
    {
        _session = session;
    }

    public async Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _session.QueryOver<UserInfo>()
            .Where(u => u.Email == email)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<UserInfo> Save(UserInfo userInfo, CancellationToken cancellationToken)
    {
        await _session.SaveAsync(userInfo, cancellationToken);

        return userInfo;
    }
}