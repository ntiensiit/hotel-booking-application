using Port.Driven.Shared.Events;

namespace Application.Commands.V1.CreateCommands.CreateReview;

public class CreateReviewCommandHandlerV1 : ICommandHandler<CreateReviewCommandV1, object>
{
    public async Task<object> Handle(CreateReviewCommandV1 request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}