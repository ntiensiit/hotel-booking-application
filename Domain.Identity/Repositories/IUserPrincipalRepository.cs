using Domain.Identity.Entities;
using SharedKernel.SeedWork;

namespace Domain.Identity.Repositories;

public interface IUserPrincipalRepository : IGenericRepository<UserPrincipal, int>
{
}