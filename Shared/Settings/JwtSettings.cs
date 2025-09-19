namespace Shared.Settings;

public record JwtSettings
{
    public const string SectionName = "Jwt";

    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
    public string? JWKS { get; set; }
}
