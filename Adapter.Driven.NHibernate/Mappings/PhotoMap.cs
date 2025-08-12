using Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace Adapter.Driven.NHibernate.Mappings;

public class PhotoMap : ClassMap<Photo>
{
    public PhotoMap()
    {
        Table("Photos");

        // Id
        Id(x => x.Id).GeneratedBy.Identity();

        // Primitive types

        // Enum types

        // Value Objects - Has One

        // Value Objects - Has Many
    }
}