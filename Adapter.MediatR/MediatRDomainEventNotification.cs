using MediatR;
using SharedKernel.SeedWork;

namespace Adapter.MediatR;

public class MediatRDomainEventNotification<TDomainEvent> : INotification where TDomainEvent : IDomainEvent
{
    public MediatRDomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}