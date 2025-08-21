using Adapter.Driven.EFCore.Contexts;
using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Adapter.Driving.AuthenticationServer.Services;

public interface IJwtTokenService
{
    Task<(string accessToken, string refreshToken)> GenerateLoginTokenAsync(string subject, string email,
        List<string> roles, string ipAddress);

    Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken, string? clientId,
        string? ipAddress);

    Task RevokeRefreshTokenAsync(string refreshToken);
}

public class JwtTokenService : JwtTokenGenerator, IJwtTokenService
{
    public JwtTokenService(IOptions<JwtSettings> jwtSettings, ApplicationDbContext dbContext) : base(jwtSettings,
        dbContext)
    {
    }

    public async Task<(string accessToken, string refreshToken)> GenerateLoginTokenAsync(string subject, string email,
        List<string> roles, string ipAddress)
    {
        var accessToken = GenerateAccessToken(subject, email, roles, out var jwtId);
        var refreshToken = GenerateRefreshToken();

        await DbContext.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = refreshToken,
            JwtId = jwtId,
            ExpiresAt = DateTime.UtcNow.AddDays(JwtSettings.RefreshTokenExpirationDays),
            CreatedAt = DateTime.UtcNow,
            UserId = int.Parse(subject),
            ClientId = null,
            IsRevoked = false,
            RevokedAt = null,
            CreatedByIp = ipAddress
        });
        await DbContext.SaveChangesAsync();

        return (accessToken, refreshToken);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string refreshToken,
        string? clientId, string? ipAddress)
    {
        var existingToken = await DbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(t => t.Token == refreshToken);

        if (existingToken is not { IsValid: true }) return default;

        existingToken.IsRevoked = true;
        existingToken.RevokedAt = DateTime.UtcNow;

        var user = existingToken.User;
        var roles = await DbContext.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(DbContext.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r.Name)
            .ToListAsync();

        var accessToken = GenerateAccessToken(user.Id.ToString(), user.Email, roles, out var jwtId);
        var newRefreshToken = GenerateRefreshToken();

        Client? client = null;
        if (!string.IsNullOrEmpty(clientId))
            client = await DbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);

        await DbContext.RefreshTokens.AddAsync(new RefreshToken
        {
            Token = newRefreshToken,
            JwtId = jwtId,
            ExpiresAt = DateTime.UtcNow.AddDays(JwtSettings.RefreshTokenExpirationDays),
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            ClientId = client?.Id,
            IsRevoked = false,
            RevokedAt = null,
            CreatedByIp = ipAddress
        });
        await DbContext.SaveChangesAsync();

        return (accessToken, newRefreshToken);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var existingToken = await DbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (existingToken is not { IsValid: true }) return;

        existingToken.IsRevoked = true;
        existingToken.RevokedAt = DateTime.UtcNow;

        await DbContext.SaveChangesAsync();
    }
}