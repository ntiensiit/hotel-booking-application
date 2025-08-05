namespace Port.Driven.Events;

public interface IQuery<out TResponse>
{
}

public interface IQueryHandler<in TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}