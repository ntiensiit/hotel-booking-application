using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
        );
    }
}