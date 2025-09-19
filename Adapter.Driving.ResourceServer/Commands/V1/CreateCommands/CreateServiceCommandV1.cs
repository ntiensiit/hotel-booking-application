using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Service;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;

public record CreateServiceCommandV1(ServiceCreateRequestBodyV1 RequestBody) : ICommand<object>;

public class CreateServiceCommandHandlerV1 : ICommandHandler<CreateServiceCommandV1, object>
{
    public async Task<object> HandleAsync(CreateServiceCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
