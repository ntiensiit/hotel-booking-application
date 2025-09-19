using Adapter.Driven.EFCore.Contexts;
using Domain.Identity.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Adapter.Driving.AuthenticationServer.Services;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(string subject, string email, List<string> roles, out string jwtId);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpirationDate();
    DateTime GetRefreshTokenExpirationDate();
}

public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings, ApplicationDbContext dbContext)
    : IJwtTokenGenerator
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    protected readonly JwtSettings JwtSettings = jwtSettings.Value;

    public string GenerateAccessToken(
        string subject,
        string email,
        List<string> roles,
        out string jwtId
    )
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        const int keySize = 2048;
        var rsa = RSA.Create(keySize);

        try
        {
            var signingKey = DbContext.SigningKeys.FirstOrDefault(k =>
                k.IsActive && !k.IsRevoked && k.ExpiresAt > DateTime.UtcNow
            );
            if (signingKey == null)
            {
                signingKey = new SigningKey
                {
                    KeyId = Guid.NewGuid().ToString(),
                    PrivateKeyBase64 = Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
                    PublicKeyBase64 = Convert.ToBase64String(rsa.ExportRSAPublicKey()),
                    Algorithm = SecurityAlgorithms.RsaSha256,
                    KeySize = keySize,
                    KeyType = JsonWebAlgorithmsKeyTypes.RSA,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.Add(TimeSpan.FromDays(365)),
                    IsRevoked = false,
                    RevokedReason = null,
                    RevokedAt = null,
                };
                DbContext.SigningKeys.Add(signingKey);
                DbContext.SaveChanges();
            }
            else
            {
                try
                {
                    rsa.ImportRSAPrivateKey(
                        Convert.FromBase64String(signingKey.PrivateKeyBase64),
                        out _
                    );
                }
                catch (CryptographicException ex)
                {
                    throw new CryptographicException(
                        "Invalid private key format. Cannot convert from string.",
                        ex
                    );
                }
            }

            jwtId = signingKey.KeyId;

            var signingSecurityKey = new RsaSecurityKey(rsa) { KeyId = signingKey.KeyId };

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, subject),
                new(JwtRegisteredClaimNames.Jti, jwtId),
                new(JwtRegisteredClaimNames.Email, email),
                new(
                    JwtRegisteredClaimNames.Iss,
                    JwtSettings.Issuer ?? throw new InvalidOperationException("Issuer is not configured")
                ),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(JwtSettings.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    signingSecurityKey,
                    SecurityAlgorithms.RsaSha256
                ),
                Issuer = JwtSettings.Issuer,
                Audience = JwtSettings.Audience,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        finally
        {
            rsa.Dispose();
        }
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public DateTime GetAccessTokenExpirationDate()
    {
        return DateTime.UtcNow.AddMinutes(JwtSettings.AccessTokenExpirationMinutes);
    }

    public DateTime GetRefreshTokenExpirationDate()
    {
        return DateTime.UtcNow.AddDays(JwtSettings.RefreshTokenExpirationDays);
    }
}
