using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class ServiceRepository : NHibernatePagingAndSortingRepository<Service<int>, int>, IServiceRepository
{
    public ServiceRepository(ISession session) : base(session)
    {
    }
}