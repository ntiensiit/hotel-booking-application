namespace Port.Driven.Events;

public interface ICommand
{
}

public interface ICommand<out TResponse>
{
}

public interface ICommandHandler<in TRequest> where TRequest : ICommand
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}