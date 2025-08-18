using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IUserInfoRepository : IGenericRepository<UserInfo<int>, int>,
    IPagingAndSortingRepository<UserInfo<int>, int>
{
    Task<UserInfo<int>?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}