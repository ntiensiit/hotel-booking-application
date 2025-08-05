using MediatR;
using Port.Driven.Events;

namespace Adapter.MediatR;

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
    MediatRCommandRequestHandler<TCommand, TResponse> : IRequestHandler<MediatRCommandRequest<TCommand, TResponse>,
    TResponse>
    where TCommand : ICommand<TResponse>
{
    private readonly ICommandHandler<TCommand, TResponse> _commandHandler;

    public MediatRCommandRequestHandler(ICommandHandler<TCommand, TResponse> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<TResponse> Handle(MediatRCommandRequest<TCommand, TResponse> request,
        CancellationToken cancellationToken)
    {
        return await _commandHandler.Handle(request.Command, cancellationToken);
    }
}