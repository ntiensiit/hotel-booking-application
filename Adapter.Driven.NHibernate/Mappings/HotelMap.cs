using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class HotelMap<TId> : ClassMap<Hotel<TId>> where TId : IEquatable<TId>
{
    public HotelMap()
    {
        Table("Hotels");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive properties
        Map(x => x.Name);
        Map(x => x.Description);
        Map(x => x.RegistrationDate);
        Map(x => x.ApprovedDate);

        // Enum properties
        Map(x => x.HotelType);
        Map(x => x.Status);

        // Value Objects - Has One
        Component(x => x.Address, m =>
        {
            m.Map(x => x.City);
            m.Component(x => x.Coordinates, c =>
            {
                c.Map(y => y.Latitude);
                c.Map(y => y.Longitude);
            });
            m.Map(x => x.Country);
            m.Map(x => x.PostalCode);
            m.Map(x => x.State);
            m.Map(x => x.Street);
        });

        Component(x => x.StarRating, m => { m.Map(x => x.Value).Column("StarRating"); });

        Component(x => x.ContactInfo, m =>
        {
            m.Component(x => x.Email, c => { c.Map(y => y.Value).Column("Email"); });
            m.Component(x => x.Phone, c =>
            {
                c.Map(y => y.CountryCode);
                c.Map(y => y.Number);
            });
            m.Map(x => x.Website);
        });

        // Value Objects - Has Many

        // Reference Ids
        Map(x => x.HostId);
    }
}