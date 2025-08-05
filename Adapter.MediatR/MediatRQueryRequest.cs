using MediatR;
using Port.Driven.Events;

namespace Adapter.MediatR;

public class MediatRQueryRequest<TQuery, TResponse> : IRequest<TResponse> where TQuery : IQuery<TResponse>
{
    public MediatRQueryRequest(TQuery query)
    {
        Query = query;
    }

    public TQuery Query { get; }
}