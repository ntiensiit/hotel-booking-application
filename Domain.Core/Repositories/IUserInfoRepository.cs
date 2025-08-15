using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IUserInfoRepository : IGenericRepository<UserInfo<int>, int>
{
    Task<UserInfo<int>?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}