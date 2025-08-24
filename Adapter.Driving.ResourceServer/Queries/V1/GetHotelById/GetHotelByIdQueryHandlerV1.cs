using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetHotelById;

public class GetHotelByIdQueryHandlerV1 : IQueryHandler<GetHotelByIdQueryV1, object?>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelByIdQueryHandlerV1(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<object?> Handle(GetHotelByIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await _hotelRepository.GetByIdAsync(request.Id);
    }
}