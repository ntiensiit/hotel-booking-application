using MediatR;
using SharedKernel.SeedWork;

namespace Adapter.MediatR;

public abstract class MediatRDomainEventNotificationHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>,
    INotificationHandler<MediatRDomainEventNotification<TDomainEvent>> where TDomainEvent : IDomainEvent
{
    public abstract Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task Handle(MediatRDomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken)
    {
        return HandleAsync(notification.DomainEvent, cancellationToken);
    }
}