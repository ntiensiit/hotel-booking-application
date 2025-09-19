using Adapter.Driven.NHibernate.Persistence;
using Domain.Core.Entities;
using NHibernate;
using Port.Driven.NHibernate;

namespace Adapter.Driven.NHibernate.Repositories;

public class ReviewRepository(ISession session) : NHibernatePagingAndSortingRepository<Review, int>(session), IReviewRepository
{
    public async Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId)
    {
        return await Session.QueryOver<Review>().Where(r => r.HotelId == hotelId).ListAsync();
    }
}
