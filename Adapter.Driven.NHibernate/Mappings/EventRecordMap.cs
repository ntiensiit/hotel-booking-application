using Adapter.Driven.NHibernate.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Adapter.Driven.NHibernate.Mappings;

public class EventRecordMap<TId> : ClassMapping<EventRecord<TId>> where TId : IEquatable<TId>
{
    public EventRecordMap()
    {
        Table("EventStore");

        Id(x => x.Id, m => { m.Generator(Generators.GuidComb); });
    }
}