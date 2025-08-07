using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRCommandRequestHandler<TCommand> : IRequestHandler<MediatRCommandRequest<TCommand>>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;

    public MediatRCommandRequestHandler(ICommandHandler<TCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task Handle(MediatRCommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        await _commandHandler.Handle(request.Command, cancellationToken);
    }
}

public class
    MediatRCommandRequestHandler<TCommand, TResult> : IRequestHandler<MediatRCommandRequest<TCommand, TResult>,
    TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _commandHandler;

    public MediatRCommandRequestHandler(ICommandHandler<TCommand, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<TResult> Handle(MediatRCommandRequest<TCommand, TResult> request,
        CancellationToken cancellationToken)
    {
        return await _commandHandler.Handle(request.Command, cancellationToken);
    }
}