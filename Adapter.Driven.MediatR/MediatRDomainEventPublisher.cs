using MediatR;
using SharedKernel.SeedWork;

namespace Adapter.Driven.MediatR;

public class MediatRDomainEventPublisher : IDomainEventPublisher
{
    private readonly IPublisher _mediator;

    public MediatRDomainEventPublisher(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEventsAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var domainEventType = domainEvent.GetType();
        var notificationType = typeof(MediatRDomainEventNotification<>).MakeGenericType(domainEventType);
        var notification = Activator.CreateInstance(notificationType, domainEvent)!;

        await _mediator.Publish((INotification)notification, cancellationToken);
    }
}