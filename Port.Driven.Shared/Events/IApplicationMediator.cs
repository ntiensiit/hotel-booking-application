namespace Port.Driven.Shared.Events;

public interface IApplicationMediator
{
    // Send command
    Task<TResult> SendCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

    Task SendCommandAsync(ICommand command, CancellationToken cancellationToken = default);

    // Send query
    Task<TResult> SendQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}
