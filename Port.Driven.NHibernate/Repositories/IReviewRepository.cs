using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IReviewRepository : IGenericRepository<Review<int>, int>, IPagingAndSortingRepository<Review<int>, int>
{
}