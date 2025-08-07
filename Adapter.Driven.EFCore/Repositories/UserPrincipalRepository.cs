using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Port.Driven.EFCore.Repositories;

namespace Adapter.Driven.EFCore.Repositories;

public class UserPrincipalRepository : IUserPrincipalRepository
{
    private readonly DbContext _dbContext;

    public UserPrincipalRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserPrincipal> SaveOrUpdate(UserPrincipal userPrincipal, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Set<UserPrincipal>()
            .AnyAsync(e => e.Id.Equals(userPrincipal.Id), cancellationToken);

        if (existingUser)
            _dbContext.Set<UserPrincipal>().Update(userPrincipal);
        else
            _dbContext.Set<UserPrincipal>().Add(userPrincipal);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return userPrincipal;
    }
}