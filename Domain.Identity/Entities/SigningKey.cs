namespace Domain.Identity.Entities;

public partial class SigningKey
{
    public string KeyId { get; set; }
    public string PrivateKeyBase64 { get; set; }
    public string PublicKeyBase64 { get; set; }
    public string Algorithm { get; set; }
    public int KeySize { get; set; }
    public string KeyType { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevokedReason { get; set; }

    // Id
    public int Id { get; }
}

public partial class SigningKey
{
    public bool IsExpired => ExpiresAt < DateTime.UtcNow;
    public bool IsValidForSigning => IsActive && !IsExpired && !IsRevoked;

    public void RevokeKey(string reason)
    {
        IsActive = false;
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        RevokedReason = reason;
    }
}