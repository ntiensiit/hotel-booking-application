using Adapter.Driven.EFCore.Contexts;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("/api/[controller]/.well-known/jwks.json")]
[ApiController]
public class JwksController(ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var validKeys = await _dbContext
            .SigningKeys.Where(k => k.IsActive && !k.IsRevoked && k.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();

        if (validKeys.Count == 0)
            return NotFound(new { message = "No active signing keys found." });

        var jwks = new { Keys = validKeys.ConvertAll(ConvertToJsonWebKey) };

        return Ok(jwks);
    }

    private JsonWebKey ConvertToJsonWebKey(SigningKey signingKey)
    {
        using var rsa = RSA.Create();
        var publicKeyBytes = Convert.FromBase64String(signingKey.PublicKeyBase64);
        rsa.ImportRSAPublicKey(publicKeyBytes, out _);

        var parameters = rsa.ExportParameters(false);

        return new JsonWebKey
        {
            Kid = signingKey.KeyId,
            Kty = signingKey.KeyType,
            Use = "sig",
            Alg = SecurityAlgorithms.RsaSha256,
            E = Base64UrlEncoder.Encode(parameters.Exponent),
            N = Base64UrlEncoder.Encode(parameters.Modulus),
        };
    }
}
