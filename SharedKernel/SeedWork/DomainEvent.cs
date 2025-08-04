using MediatR;

namespace SharedKernel.SeedWork;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; init; }
}

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
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
    Task DispatchEventsAsync<T>(T aggregate) where T : IHasDomainEvent;
}

public interface IDomainEventHandler
{
    Task HandleAsync(IDomainEvent domainEvent);
}