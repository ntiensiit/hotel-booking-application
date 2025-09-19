namespace Port.Driven.Shared.Events;

public interface IQuery<out TResult>;

public interface IQueryHandler<in TRequest, TResult> where TRequest : IQuery<TResult>
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
