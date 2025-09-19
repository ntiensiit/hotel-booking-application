using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
            },
            new
            {
                Id = 2,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
            },
            new
            {
                Id = 3,
                Name = "Host",
                NormalizedName = "HOST",
            }
        );
    }
}
