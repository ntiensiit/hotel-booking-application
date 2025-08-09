using Adapter.Driven.EFCore.Persistence;
using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore.Repositories;

namespace Adapter.Driven.EFCore.Repositories;

public class UserPrincipalRepository : EfCoreGenericRepository<UserPrincipal, int>, IUserPrincipalRepository
{
    public UserPrincipalRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserPrincipal> SaveOrUpdate(UserPrincipal userPrincipal, CancellationToken cancellationToken)
    {
        var existingUser = await ContainsAsync(userPrincipal, cancellationToken);

        if (existingUser)
            DbContext.Set<UserPrincipal>().Update(userPrincipal);
        else
            DbContext.Set<UserPrincipal>().Add(userPrincipal);

        await DbContext.SaveChangesAsync(cancellationToken);

        return userPrincipal;
    }
}