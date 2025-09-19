using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasData(
            new { UserId = 1, RoleId = 1 }, // Admin
            new { UserId = 2, RoleId = 2 }, // Customer
            new { UserId = 3, RoleId = 3 } // Host
        );
    }
}
