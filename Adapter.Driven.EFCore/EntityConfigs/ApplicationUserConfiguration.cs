using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                PhoneNumber = "",
                PasswordHash = hasher.HashPassword(new ApplicationUser(), "Admin@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            },
            new ApplicationUser
            {
                Id = 2,
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@example.com",
                NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
                PhoneNumber = "",
                PasswordHash = hasher.HashPassword(new ApplicationUser(), "Customer@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            },
            new ApplicationUser
            {
                Id = 3,
                UserName = "host",
                NormalizedUserName = "HOST",
                Email = "host@example.com",
                NormalizedEmail = "HOST@EXAMPLE.COM",
                PhoneNumber = "",
                PasswordHash = hasher.HashPassword(new ApplicationUser(), "Host@123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            }
        );
    }
}
