using MediatR;
using Port.Driven.Events;

namespace Infrastructure.MediatR;

public class MediatRDomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    public MediatRDomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}