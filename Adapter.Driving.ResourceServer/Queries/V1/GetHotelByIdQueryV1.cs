using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetHotelByIdQueryV1(int Id) : IQuery<object?>;

public class GetHotelByIdQueryHandlerV1(IHotelRepository hotelRepository) : IQueryHandler<GetHotelByIdQueryV1, object?>
{
    public async Task<object?> HandleAsync(GetHotelByIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await hotelRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
