using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRCommandRequest<TCommand> : IRequest where TCommand : ICommand
{
    public MediatRCommandRequest(TCommand command)
    {
        Command = command;
    }

    public TCommand Command { get; }
}

public class MediatRCommandRequest<TCommand, TResult> : IRequest<TResult> where TCommand : ICommand<TResult>
{
    public MediatRCommandRequest(TCommand command)
    {
        Command = command;
    }

    public TCommand Command { get; }
}