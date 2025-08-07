namespace Port.Driven.Shared.Events;

public interface IApplicationMediator
{
    Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;

    Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResponse>;

    Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>;
}