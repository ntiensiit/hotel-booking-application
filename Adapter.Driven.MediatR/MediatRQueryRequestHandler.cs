using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class
    MediatRQueryRequestHandler<TQuery, TResult> : IRequestHandler<MediatRQueryRequest<TQuery, TResult>,
    TResult> where TQuery : IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _commandHandler;

    public MediatRQueryRequestHandler(IQueryHandler<TQuery, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<TResult> Handle(MediatRQueryRequest<TQuery, TResult> request,
        CancellationToken cancellationToken)
    {
        return await _commandHandler.Handle(request.Query, cancellationToken);
    }
}