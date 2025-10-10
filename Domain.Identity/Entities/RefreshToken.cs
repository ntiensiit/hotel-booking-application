using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public partial class RefreshToken : Entity<int>
{
    public required string Token { get; set; }
    public required string JwtId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedByIp { get; set; }

    // Id
    public int Id { get; set; }
}

public partial class RefreshToken
{
    public int UserId { get; set; }
    public int? ClientId { get; set; }
}

public partial class RefreshToken
{
    public virtual ApplicationUser User { get; set; }
    public virtual Client? Client { get; set; }
}

public partial class RefreshToken
{
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsValid => !IsRevoked && !IsExpired;
}
