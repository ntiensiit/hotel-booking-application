using Domain.Core.Entities;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetHotelsByPagingQueryV1(int PageNumber, int PageSize) : IQuery<IPage<Hotel>>;

public class GetHotelsByPagingQueryHandlerV1(IHotelRepository hotelRepository) : IQueryHandler<GetHotelsByPagingQueryV1, IPage<Hotel>>
{
    public async Task<IPage<Hotel>> HandleAsync(GetHotelsByPagingQueryV1 request, CancellationToken cancellationToken)
    {
        return await hotelRepository.FindAllAsync(PageRequest.Of(request.PageNumber, request.PageSize), cancellationToken: cancellationToken);
    }
}
