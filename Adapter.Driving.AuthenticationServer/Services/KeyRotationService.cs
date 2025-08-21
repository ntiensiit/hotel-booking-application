using System.Security.Cryptography;
using Adapter.Driven.EFCore.Contexts;
using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Adapter.Driving.AuthenticationServer.Services;

public class KeyRotationService : BackgroundService
{
    private readonly TimeSpan _rotationInterval;

    private readonly IServiceProvider _serviceProvider;

    public KeyRotationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _rotationInterval = TimeSpan.FromDays(7);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RotateKeysAsync();

            await Task.Delay(_rotationInterval, stoppingToken);
        }
    }

    private async Task RotateKeysAsync()
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var activeKey =
            await dbContext.SigningKeys.FirstOrDefaultAsync(k =>
                k.IsActive && !k.IsRevoked && k.ExpiresAt > DateTime.UtcNow);

        if (activeKey == null || activeKey.ExpiresAt <= DateTime.UtcNow.AddDays(10))
        {
            if (activeKey != null)
            {
                activeKey.RevokeKey("Rotated by key rotation service");

                dbContext.SigningKeys.Update(activeKey);
            }

            const int keySize = 2048;
            using var rsa = RSA.Create(keySize);
            try
            {
                var newKey = new SigningKey
                {
                    KeyId = Guid.NewGuid().ToString(),
                    PrivateKeyBase64 = Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
                    PublicKeyBase64 = Convert.ToBase64String(rsa.ExportRSAPublicKey()),
                    Algorithm = SecurityAlgorithms.RsaSha256,
                    KeySize = keySize,
                    KeyType = JsonWebAlgorithmsKeyTypes.RSA,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddYears(1),
                    IsRevoked = false,
                    RevokedReason = null,
                    RevokedAt = null
                };

                await dbContext.SigningKeys.AddAsync(newKey);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to rotate the key: {ex.Message}");
            }
        }
    }
}