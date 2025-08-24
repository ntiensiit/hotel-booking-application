using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateRoom;

public class CreateRoomCommandHandlerV1 : ICommandHandler<CreateRoomCommandV1, object>
{
    public async Task<object> Handle(CreateRoomCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}