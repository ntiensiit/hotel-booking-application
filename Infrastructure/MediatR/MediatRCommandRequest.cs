using MediatR;
using Port.Driven.Events;

namespace Infrastructure.MediatR;

public class MediatRCommandRequest<TCommand> : IRequest where TCommand : ICommand
{
    public MediatRCommandRequest(TCommand command)
    {
        Command = command;
    }

    public TCommand Command { get; }
}

public class MediatRCommandRequest<TCommand, TResponse> : IRequest<TResponse> where TCommand : ICommand<TResponse>
{
    public MediatRCommandRequest(TCommand command)
    {
        Command = command;
    }

    public TCommand Command { get; }
}