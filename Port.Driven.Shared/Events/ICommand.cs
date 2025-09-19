namespace Port.Driven.Shared.Events;

public interface ICommand;

public interface ICommand<out TResult> : ICommand;

public interface ICommandHandler<in TRequest> where TRequest : ICommand
{
    Task HandleAsync(TRequest request, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TRequest, TResult> where TRequest : ICommand<TResult>
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
