using Adapter.Driven.EFCore.Persistence;
using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Adapter.Driven.EFCore.Repositories;

public class UserPrincipalRepository : EfCoreGenericRepository<UserPrincipal, int>, IUserPrincipalRepository
{
    public UserPrincipalRepository(DbContext dbContext) : base(dbContext)
    {
    }
}