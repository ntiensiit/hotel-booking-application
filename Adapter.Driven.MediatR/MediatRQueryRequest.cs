using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRQueryRequest<TQuery, TResult>(TQuery query) : IRequest<TResult>
    where TQuery : IQuery<TResult>
{
    public TQuery Query { get; } = query;
}

public class MediatRQueryRequestHandler<TQuery, TResult>(
    IQueryHandler<TQuery, TResult> queryHandler
) : IRequestHandler<MediatRQueryRequest<TQuery, TResult>, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Handle(
        MediatRQueryRequest<TQuery, TResult> request,
        CancellationToken cancellationToken
    )
    {
        return await queryHandler.HandleAsync(request.Query, cancellationToken);
    }
}
