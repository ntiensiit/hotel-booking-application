using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRCommandRequest<TCommand>(TCommand command) : IRequest
    where TCommand : ICommand
{
    public TCommand Command { get; } = command;
}

public class MediatRCommandRequest<TCommand, TResult>(TCommand command) : IRequest<TResult>
    where TCommand : ICommand<TResult>
{
    public TCommand Command { get; } = command;
}

public class MediatRCommandRequestHandler<TCommand>(ICommandHandler<TCommand> commandHandler)
    : IRequestHandler<MediatRCommandRequest<TCommand>>
    where TCommand : ICommand
{
    public async Task Handle(
        MediatRCommandRequest<TCommand> request,
        CancellationToken cancellationToken
    )
    {
        await commandHandler.HandleAsync(request.Command, cancellationToken);
    }
}

public class MediatRCommandRequestHandler<TCommand, TResult>(
    ICommandHandler<TCommand, TResult> commandHandler
) : IRequestHandler<MediatRCommandRequest<TCommand, TResult>, TResult>
    where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(
        MediatRCommandRequest<TCommand, TResult> request,
        CancellationToken cancellationToken
    )
    {
        return await commandHandler.HandleAsync(request.Command, cancellationToken);
    }
}
