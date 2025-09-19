using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class UserInfoRepository(ISession session) : NHibernatePagingAndSortingRepository<UserInfo, int>(session), IUserInfoRepository
{
    public async Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Session
            .QueryOver<UserInfo>()
            .Where(u => u.Email == email)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
