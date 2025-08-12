using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class ServiceRepository : NhibernateGenericRepository<Service, int>, IServiceRepository
{
    public ServiceRepository(ISession session) : base(session)
    {
    }
}