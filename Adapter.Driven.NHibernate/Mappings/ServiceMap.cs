using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class ServiceMap : ClassMap<Service>
{
    public ServiceMap()
    {
        Table("Services");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive properties
        Map(x => x.Name);
        Map(x => x.Description);
        Map(x => x.IsAvailable);

        // Enum properties
        Map(x => x.Category);

        // Value Objects - Has One
        Component(x => x.Price, m =>
        {
            m.Map(x => x.Amount);
            m.Map(x => x.Currency);
        });

        // Value Objects - Has Many

        // Reference Ids
        Map(x => x.HotelId);
    }
}