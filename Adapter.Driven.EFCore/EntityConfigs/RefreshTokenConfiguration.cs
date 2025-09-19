using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(rt => rt.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Ignore(rt => rt.IsExpired);
        builder.Ignore(rt => rt.IsValid);

        builder
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rt => rt.Client)
            .WithMany()
            .HasForeignKey(rt => rt.ClientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
