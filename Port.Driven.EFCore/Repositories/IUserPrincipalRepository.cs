using Domain.Identity.Entities;
using SharedKernel.SeedWork;

namespace Port.Driven.EFCore.Repositories;

public interface IUserPrincipalRepository : IGenericRepository<UserPrincipal, int>
{
    public Task<UserPrincipal> SaveOrUpdate(UserPrincipal userPrincipal, CancellationToken cancellationToken);
}