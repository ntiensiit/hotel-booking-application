using Domain.Core.Entities;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetHotelsByPaging;

public record GetHotelsByPagingQueryV1(int PageNumber, int PageSize) : IQuery<IPage<Hotel<int>>>;