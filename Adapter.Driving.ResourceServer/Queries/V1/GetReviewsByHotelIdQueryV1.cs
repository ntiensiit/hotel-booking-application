using Domain.Core.Entities;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetReviewsByHotelIdQueryV1(int HotelId) : IQuery<IEnumerable<Review>>;

public class GetReviewsByHotelIdQueryHandlerV1(IReviewRepository reviewRepository) : IQueryHandler<GetReviewsByHotelIdQueryV1, IEnumerable<Review>>
{
    public async Task<IEnumerable<Review>> HandleAsync(GetReviewsByHotelIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await reviewRepository.GetReviewsByHotelIdAsync(request.HotelId);
    }
}
