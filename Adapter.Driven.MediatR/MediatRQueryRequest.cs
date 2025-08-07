using MediatR;
using Port.Driven.Shared.Events;

namespace Adapter.Driven.MediatR;

public class MediatRQueryRequest<TQuery, TResult> : IRequest<TResult> where TQuery : IQuery<TResult>
{
    public MediatRQueryRequest(TQuery query)
    {
        Query = query;
    }

    public TQuery Query { get; }
}