using Domain.Core.Entities;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetReviewsByHotelId;

public class GetReviewsByHotelIdQueryHandlerV1 : IQueryHandler<GetReviewsByHotelIdQueryV1, IEnumerable<Review<int>>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewsByHotelIdQueryHandlerV1(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review<int>>> Handle(GetReviewsByHotelIdQueryV1 request,
        CancellationToken cancellationToken)
    {
        return await _reviewRepository.GetReviewsByHotelIdAsync(request.HotelId);
    }
}