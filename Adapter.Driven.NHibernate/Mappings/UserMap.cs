using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class UserMap : ClassMap<UserInfo>
{
    public UserMap()
    {
        Table("UserInfo");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive types
        Map(x => x.FullName);
        Map(x => x.DateOfBirth);

        // Enum types

        // Value Objects - Has One
        Component(x => x.Email, m => { m.Map(x => x.Value).Column("Email"); });

        Component(x => x.PhoneNumber, m =>
        {
            m.Map(x => x.CountryCode);
            m.Map(x => x.Number);
        });

        // Value Objects - Has Many
    }
}