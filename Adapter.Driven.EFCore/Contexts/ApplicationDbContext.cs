using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adapter.Driven.EFCore.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        int,
        ApplicationUserClaims,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaims,
        ApplicationUserToken
    >(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<SigningKey> SigningKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
