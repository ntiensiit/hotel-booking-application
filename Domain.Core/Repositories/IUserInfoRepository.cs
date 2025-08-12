using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IUserInfoRepository : IGenericRepository<UserInfo, int>
{
    Task<UserInfo?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}