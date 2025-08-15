using Domain.Core.Entities;
using SharedKernel.SeedWork;

namespace Domain.Core.Repositories;

public interface IReviewRepository : IGenericRepository<Review<int>, int>
{
}