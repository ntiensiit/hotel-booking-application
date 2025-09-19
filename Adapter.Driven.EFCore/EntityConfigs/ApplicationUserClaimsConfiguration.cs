using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationUserClaimsConfiguration : IEntityTypeConfiguration<ApplicationUserClaims>
{
    public void Configure(EntityTypeBuilder<ApplicationUserClaims> builder) { }
}
