using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Room;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;

public record CreateRoomCommandV1(RoomCreateRequestBodyV1 RequestBody) : ICommand<object>;

public class CreateRoomCommandHandlerV1 : ICommandHandler<CreateRoomCommandV1, object>
{
    public async Task<object> HandleAsync(CreateRoomCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
