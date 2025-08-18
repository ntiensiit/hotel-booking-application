using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class ReviewRepository : NHibernatePagingAndSortingRepository<Review<int>, int>, IReviewRepository
{
    public ReviewRepository(ISession session) : base(session)
    {
    }
}