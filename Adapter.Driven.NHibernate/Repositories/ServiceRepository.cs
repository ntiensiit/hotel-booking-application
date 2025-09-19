using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class ServiceRepository(ISession session) : NHibernatePagingAndSortingRepository<Service, int>(session), IServiceRepository;
