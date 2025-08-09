using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class UserInfoRepository : NhibernateGenericRepository<UserInfo, int>, IUserInfoRepository
{
    public UserInfoRepository(ISession session) : base(session)
    {
    }

    public async Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Session.QueryOver<UserInfo>()
            .Where(u => u.Email == email)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void Save(UserInfo userInfo)
    {
        Session.Save(userInfo);
    }
}