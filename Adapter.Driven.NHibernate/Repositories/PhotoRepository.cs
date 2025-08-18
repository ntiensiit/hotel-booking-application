using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class PhotoRepository : NHibernatePagingAndSortingRepository<Photo<int>, int>, IPhotoRepository
{
    public PhotoRepository(ISession session) : base(session)
    {
    }
}