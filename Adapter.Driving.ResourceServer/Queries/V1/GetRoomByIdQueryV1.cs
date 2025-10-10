using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1;

public record GetRoomByIdQueryV1(int Id) : IQuery<object?>;

public class GetRoomByIdQueryHandlerV1(IRoomRepository roomRepository) : IQueryHandler<GetRoomByIdQueryV1, object?>
{
    public async Task<object?> HandleAsync(GetRoomByIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await roomRepository.FindByIdAsync(request.Id, cancellationToken);
    }
}
