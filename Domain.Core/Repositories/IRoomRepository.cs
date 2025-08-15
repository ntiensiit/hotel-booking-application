using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IRoomRepository : IGenericRepository<Room<int>, int>
{
}