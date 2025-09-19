using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ApplicationRoleClaimsConfiguration : IEntityTypeConfiguration<ApplicationRoleClaims>
{
    public void Configure(EntityTypeBuilder<ApplicationRoleClaims> builder) { }
}
