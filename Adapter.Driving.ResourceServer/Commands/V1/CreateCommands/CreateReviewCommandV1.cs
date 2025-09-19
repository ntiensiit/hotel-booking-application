using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Review;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;

public record CreateReviewCommandV1(ReviewCreateRequestBodyV1 RequestBody) : ICommand<object>;

public class CreateReviewCommandHandlerV1 : ICommandHandler<CreateReviewCommandV1, object>
{
    public async Task<object> HandleAsync(CreateReviewCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
