using Domain.Core.Entities;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetHotelsByPaging;

public class GetHotelsByPagingQueryHandlerV1 : IQueryHandler<GetHotelsByPagingQueryV1, IPage<Hotel<int>>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelsByPagingQueryHandlerV1(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<IPage<Hotel<int>>> Handle(GetHotelsByPagingQueryV1 request, CancellationToken cancellationToken)
    {
        return await _hotelRepository.FindAllAsync(PageRequest.Of(request.PageNumber, request.PageSize));
    }
}