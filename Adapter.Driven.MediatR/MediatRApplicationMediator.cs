using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRApplicationMediator : IApplicationMediator
{
    private readonly IMediator _mediator;

    public MediatRApplicationMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        await _mediator.Send(new MediatRCommandRequest<TCommand>(command), cancellationToken);
    }

    public async Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command,
        CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
    {
        return await _mediator.Send(new MediatRCommandRequest<TCommand, TResponse>(command), cancellationToken);
    }

    public async Task<TResponse> SendQueryAsync<TQuery, TResponse>(TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>
    {
        return await _mediator.Send(new MediatRQueryRequest<TQuery, TResponse>(query), cancellationToken);
    }
}