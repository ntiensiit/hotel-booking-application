using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IHotelRepository : IGenericRepository<Hotel<int>, int>
{
}