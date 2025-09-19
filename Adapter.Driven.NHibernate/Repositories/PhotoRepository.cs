using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class PhotoRepository(ISession session) : NHibernatePagingAndSortingRepository<Photo, int>(session), IPhotoRepository;
