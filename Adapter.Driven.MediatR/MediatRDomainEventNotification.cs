using MediatR;
using SharedKernel.SeedWork;

namespace Adapter.Driven.MediatR;

public class MediatRDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification
    where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}

public abstract class MediatRDomainEventNotificationHandler<TDomainEvent>
    : IDomainEventHandler<TDomainEvent>,
        INotificationHandler<MediatRDomainEventNotification<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    public abstract Task HandleAsync(
        TDomainEvent domainEvent,
        CancellationToken cancellationToken = default
    );

    public Task Handle(
        MediatRDomainEventNotification<TDomainEvent> notification,
        CancellationToken cancellationToken
    )
    {
        return HandleAsync(notification.DomainEvent, cancellationToken);
    }
}

public class MediatRDomainEventAdapter<TDomainEvent>(
    IEnumerable<IDomainEventHandler<TDomainEvent>> handlers
) : INotificationHandler<MediatRDomainEventNotification<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    public async Task Handle(
        MediatRDomainEventNotification<TDomainEvent> notification,
        CancellationToken cancellationToken
    )
    {
        await Task.WhenAll(
            handlers.Select(handler =>
                handler.HandleAsync(notification.DomainEvent, cancellationToken)
            )
        );
    }
}

public class MediatRDomainEventPublisher(IPublisher mediator) : IDomainEventPublisher
{
    public async Task PublishEventsAsync(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken = default
    )
    {
        var domainEventType = domainEvent.GetType();
        var notificationType = typeof(MediatRDomainEventNotification<>).MakeGenericType(
            domainEventType
        );
        var notification = Activator.CreateInstance(notificationType, domainEvent)!;

        await mediator.Publish((INotification)notification, cancellationToken);
    }
}
