using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class ReviewMap : ClassMap<Review>
{
    public ReviewMap()
    {
        Table("Reviews");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive properties
        Map(x => x.IsVerified);
        Map(x => x.Comment);

        // Enum properties

        // Value Objects - Has One
        Component(x => x.Rating, m =>
        {
            m.Map(x => x.CleanlinessRating);
            m.Map(x => x.LocationRating);
            m.Map(x => x.OverallRating);
            m.Map(x => x.ServiceRating);
            m.Map(x => x.ValueRating);
        });

        // Value Objects - Has Many

        // Reference Ids
        Map(x => x.UserId);
        Map(x => x.HotelId);
        Map(x => x.BookingId);
    }
}