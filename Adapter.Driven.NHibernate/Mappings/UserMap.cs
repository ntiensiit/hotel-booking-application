using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class UserMap : ClassMap<UserInfo>
{
    public UserMap()
    {
        Table("UserInfo");

        Id(x => x.Id).GeneratedBy.Identity();

        Map(x => x.FullName).Not.Nullable().Length(255);
        Map(x => x.Age);
        Map(x => x.Email).Unique().Length(255);
        Map(x => x.PhoneNumber).Length(50);
    }
}