using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetReviewsByHotelId;

public record GetReviewsByHotelIdQueryV1(int HotelId) : IQuery<IEnumerable<Review<int>>>;