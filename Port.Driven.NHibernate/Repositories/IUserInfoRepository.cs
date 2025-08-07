using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IUserInfoRepository : IGenericRepository<UserInfo, int>
{
    Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    void Save(UserInfo userInfo);
}