using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRApplicationMediator(IMediator mediator) : IApplicationMediator
{
    // Send Command
    public Task SendCommandAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var requestType = typeof(MediatRCommandRequest<>).MakeGenericType(commandType);
        var request = Activator.CreateInstance(requestType, command)!;

        return mediator.Send(request, cancellationToken);
    }

    public async Task<TResult> SendCommandAsync<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default
    )
    {
        var commandType = command.GetType();
        var requestType = typeof(MediatRCommandRequest<,>).MakeGenericType(
            commandType,
            typeof(TResult)
        );
        var request = Activator.CreateInstance(requestType, command)!;

        var result = await mediator.Send(request, cancellationToken);
        return (TResult)result!;
    }

    // Send Query
    public async Task<TResult> SendQueryAsync<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default
    )
    {
        var queryType = query.GetType();
        var requestType = typeof(MediatRQueryRequest<,>).MakeGenericType(
            queryType,
            typeof(TResult)
        );
        var request = Activator.CreateInstance(requestType, query)!;

        var result = await mediator.Send(request, cancellationToken);
        return (TResult)result!;
    }
}
