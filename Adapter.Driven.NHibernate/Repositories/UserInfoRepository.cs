using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class UserInfoRepository : NHibernatePagingAndSortingRepository<UserInfo<int>, int>, IUserInfoRepository
{
    public UserInfoRepository(ISession session) : base(session)
    {
    }

    public async Task<UserInfo<int>?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Session.QueryOver<UserInfo<int>>()
            .Where(u => u.Email == email)
            .SingleOrDefaultAsync(cancellationToken);
    }
}