namespace SharedKernel.SeedWork;

public interface IDomainEvent
{
    DateTime OccurredOn { get; init; }
}

public interface IDomainEvent<TId> : IDomainEvent
{
    TId Id { get; init; }
}

public interface IHasDomainEvent
{
    IList<IDomainEvent> DomainEvents { init; }

    IReadOnlyCollection<IDomainEvent> GetDomainEvents();

    // void LoadFromHistory(IEnumerable<IDomainEvent> history);
    // void ApplyChange(IDomainEvent newEvent);
    void ClearDomainEvents();
    // void When(IDomainEvent domainEvent);
}

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync<T>(T aggregate)
        where T : IHasDomainEvent;
}

public interface IDomainEventPublisher
{
    Task PublishEventsAsync(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken = default
    );
}

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

public class DomainEventDispatcher(IDomainEventPublisher eventPublisher) : IDomainEventDispatcher
{
    private readonly IDomainEventPublisher _eventPublisher = eventPublisher;

    public async Task DispatchEventsAsync<T>(T aggregate)
        where T : IHasDomainEvent
    {
        await Task.WhenAll(
            aggregate
                .GetDomainEvents()
                .Select(domainEvent => _eventPublisher.PublishEventsAsync(domainEvent))
        );

        aggregate.ClearDomainEvents();
    }
}
