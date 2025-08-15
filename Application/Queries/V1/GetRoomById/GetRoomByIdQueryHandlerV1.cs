using Port.Driven.Shared.Events;

namespace Application.Queries.V1.GetRoomById;

public class GetRoomByIdQueryHandlerV1 : IQueryHandler<GetRoomByIdQueryV1, object>
{
    public async Task<object> Handle(GetRoomByIdQueryV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}