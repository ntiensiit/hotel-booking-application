using MediatR;
using Port.Driven.Events;

namespace Infrastructure.MediatR;

public class MediatRDomainEventAdapter<TDomainEvent> :
    INotificationHandler<MediatRDomainEventNotification<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    private readonly IEnumerable<IDomainEventHandler<TDomainEvent>> _handlers;

    public MediatRDomainEventAdapter(IEnumerable<IDomainEventHandler<TDomainEvent>> handlers)
    {
        _handlers = handlers;
    }

    public async Task Handle(MediatRDomainEventNotification<TDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers) await handler.HandleAsync(notification.DomainEvent, cancellationToken);
    }
}