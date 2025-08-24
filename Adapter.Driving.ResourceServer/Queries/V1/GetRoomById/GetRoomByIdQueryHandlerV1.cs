using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Queries.V1.GetRoomById;

public class GetRoomByIdQueryHandlerV1 : IQueryHandler<GetRoomByIdQueryV1, object?>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdQueryHandlerV1(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<object?> Handle(GetRoomByIdQueryV1 request, CancellationToken cancellationToken)
    {
        return await _roomRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}