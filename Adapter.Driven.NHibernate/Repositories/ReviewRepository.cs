using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class ReviewRepository : NhibernateGenericRepository<Review<int>, int>, IReviewRepository
{
    public ReviewRepository(ISession session) : base(session)
    {
    }
}