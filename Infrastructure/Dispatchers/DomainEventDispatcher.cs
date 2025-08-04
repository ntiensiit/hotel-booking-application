using MediatR;
using SharedKernel.SeedWork;

namespace Infrastructure.Dispatchers;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _mediator;

    public DomainEventDispatcher(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEventsAsync<T>(T aggregate) where T : IHasDomainEvent
    {
        var domainEvents = aggregate.GetDomainEvents().ToList();

        foreach (var domainEvent in domainEvents) await _mediator.Publish(domainEvent);

        aggregate.ClearDomainEvents();
    }
}