using Domain.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Port.Driven.NHibernate;
using System.Globalization;
using System.Security.Claims;

namespace Adapter.Driving.ResourceServer.Helpers;

public interface IHttpUserContextService
{
    string? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    Task<UserInfo?> GetUserInfoAsync();
    Task AddAuthenticatedUserToContext(TokenValidatedContext context);
}

public class UserContextService(
    IHttpContextAccessor httpContextAccessor,
    IUserInfoRepository userInfoRepository
) : IHttpUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUserInfoRepository _userInfoRepository = userInfoRepository;

    public string? UserId =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];

    public async Task<UserInfo?> GetUserInfoAsync()
    {
        if (!IsAuthenticated || UserId == null)
            return null;

        if (_httpContextAccessor.HttpContext?.Items.TryGetValue("User", out var userObj) == true && userObj is UserInfo cachedUser)
            return cachedUser;

        return await _userInfoRepository.FindByIdAsync(int.Parse(UserId));
    }

    public async Task AddAuthenticatedUserToContext(TokenValidatedContext context)
    {
        try
        {
            var claimPrincipal = context.Principal;
            if (claimPrincipal == null)
            {
                context.Fail("Principal is null");
                return;
            }

            var userIdClaim =
                claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? claimPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                context.Fail("User ID claim not found");
                return;
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                context.Fail("Invalid User ID format");
                return;
            }

            var user = await _userInfoRepository.FindByIdAsync(userId);
            if (user == null)
            {
                context.Fail("User not found in database");
                return;
            }

            var roles = claimPrincipal.FindAll(ClaimTypes.Role);

            var claimsIdentity = new ClaimsIdentity(claimPrincipal.Identity);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.DateOfBirth,user.DateOfBirth.ToString(CultureInfo.InvariantCulture)),
                new(ClaimTypes.Name, user.FullName),
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r.Value)));

            claimsIdentity.AddClaims(claims);

            var newPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.Principal = newPrincipal;
            context.HttpContext.User = newPrincipal;

            context.HttpContext.Items["User"] = user;
            context.HttpContext.Items["Roles"] = roles;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding user to context: {e.Message}");
            context.Fail($"Authentication error: {e.Message}");
        }
    }
}
