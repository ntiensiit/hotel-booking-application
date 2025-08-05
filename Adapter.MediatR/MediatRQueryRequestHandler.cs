using MediatR;
using Port.Driven.Events;

namespace Adapter.MediatR;

public class
    MediatRQueryRequestHandler<TQuery, TResponse> : IRequestHandler<MediatRQueryRequest<TQuery, TResponse>,
    TResponse> where TQuery : IQuery<TResponse>
{
    private readonly IQueryHandler<TQuery, TResponse> _commandHandler;

    public MediatRQueryRequestHandler(IQueryHandler<TQuery, TResponse> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<TResponse> Handle(MediatRQueryRequest<TQuery, TResponse> request,
        CancellationToken cancellationToken)
    {
        return await _commandHandler.Handle(request.Query, cancellationToken);
    }
}