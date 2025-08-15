using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IServiceRepository : IGenericRepository<Service<int>, int>
{
}