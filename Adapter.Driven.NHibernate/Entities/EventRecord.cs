using SharedKernel.SeedWork;
using System.Text.Json;

namespace Adapter.Driven.NHibernate.Entities;

public class EventRecord<TId> where TId : IEquatable<TId>
{
    protected EventRecord() { }

    public EventRecord(
        TId aggregateId,
        IDomainEvent domainEvent,
        int? aggregateVersion = null,
        string? eventMetaData = null
    )
    {
        Id = Guid.NewGuid();
        AggregateId = aggregateId;
        OccurredOn = domainEvent.OccurredOn;
        EventType = domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName;
        EventData = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());

        if (aggregateVersion.HasValue)
            AggregateVersion = aggregateVersion.Value;
        EventMetaData = eventMetaData;
    }

    public virtual Guid Id { get; protected set; }

    public virtual TId AggregateId { get; protected set; }

    public virtual int AggregateVersion { get; protected set; }

    public virtual string EventType { get; protected set; }

    public virtual string EventData { get; protected set; }

    public virtual string? EventMetaData { get; protected set; }

    public virtual DateTime OccurredOn { get; init; }

    public IDomainEvent DeserializeEvent()
    {
        if (string.IsNullOrEmpty(EventType) || string.IsNullOrEmpty(EventData))
            throw new InvalidOperationException(
                "Cannot deserialize event: EventType or EventData is missing."
            );

        var type =
            Type.GetType(EventType)
            ?? throw new TypeLoadException($"Could not load type for event: {EventType}.");
        return (IDomainEvent)JsonSerializer.Deserialize(EventData, type);
    }
}
