using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using NHibernate.Linq;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driven.NHibernate.Repositories;

public class ReviewRepository : NHibernatePagingAndSortingRepository<Review<int>, int>, IReviewRepository
{
    public ReviewRepository(ISession session) : base(session)
    {
    }

    public async Task<IEnumerable<Review<int>>> GetReviewsByHotelIdAsync(int hotelId)
    {
        return await Session.Query<Review<int>>().Where(r => r.HotelId == hotelId).ToListAsync();
    }
}