using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class PhotoRepository : NhibernateGenericRepository<Photo<int>, int>, IPhotoRepository
{
    public PhotoRepository(ISession session) : base(session)
    {
    }
}