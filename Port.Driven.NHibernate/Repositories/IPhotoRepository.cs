using Domain.Core.Entities;
using Port.Driven.Shared.Persistence;
using SharedKernel.SeedWork;

namespace Port.Driven.NHibernate.Repositories;

public interface IPhotoRepository : IGenericRepository<Photo<int>, int>, IPagingAndSortingRepository<Photo<int>, int>
{
}