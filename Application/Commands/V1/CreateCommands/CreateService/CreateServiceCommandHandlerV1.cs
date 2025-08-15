using Port.Driven.Shared.Events;

namespace Application.Commands.V1.CreateCommands.CreateService;

public class CreateServiceCommandHandlerV1 : ICommandHandler<CreateServiceCommandV1, object>
{
    public async Task<object> Handle(CreateServiceCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}