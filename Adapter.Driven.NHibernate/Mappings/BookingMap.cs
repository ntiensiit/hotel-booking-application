using Adapter.Driven.NHibernate.Converters;
using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class BookingMap : ClassMap<Booking>
{
    public BookingMap()
    {
        Table("Bookings");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive properties
        Map(x => x.NumberOfGuests);
        Map(x => x.CheckInDate).CustomType<DateOnlyUserType>();
        Map(x => x.CheckOutDate).CustomType<DateOnlyUserType>();

        // Enum properties
        Map(x => x.Status);

        // Value Objects - Has One
        Component(x => x.TotalAmount, m =>
        {
            m.Map(x => x.Amount);
            m.Map(x => x.Currency);
        });

        // Value Objects - Has Many
        HasMany(x => x.SpecialRequests)
            .KeyColumn("BookingId")
            .Cascade.AllDeleteOrphan()
            .AsBag()
            .Table("BookingSpecialRequests")
            .Component(m =>
            {
                m.Map(x => x.Description);
                m.Map(x => x.IsFulFilled);
                m.Map(x => x.Type);
            });

        // Reference Ids
        Map(x => x.UserId);
        Map(x => x.HotelId);
        Map(x => x.RoomId);
    }
}