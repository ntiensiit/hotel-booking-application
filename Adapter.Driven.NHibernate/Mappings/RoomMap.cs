using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class RoomMap : ClassMap<Room>
{
    public RoomMap()
    {
        Table("Rooms");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive properties
        Map(x => x.RoomNumber);
        Map(x => x.Capacity);

        // Enum properties
        Map(x => x.RoomType);
        Map(x => x.Status);

        // Value Objects - Has One
        Component(x => x.Pricing, m =>
        {
            m.Component(x => x.BasePrice, c =>
            {
                c.Map(y => y.Amount);
                c.Map(y => y.Currency);
            });
        });

        // Value Objects - Has Many

        // Reference Ids
        Map(x => x.HotelId);
    }
}