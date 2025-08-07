using Domain.Identity.Entities;

namespace Port.Driven.EFCore.Repositories;

public interface IUserPrincipalRepository
{
    public Task<UserPrincipal> SaveOrUpdate(UserPrincipal userPrincipal, CancellationToken cancellationToken);
}