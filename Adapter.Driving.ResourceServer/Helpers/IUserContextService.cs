using System.Globalization;
using System.Security.Claims;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Port.Driven.NHibernate.Repositories;

namespace Adapter.Driving.ResourceServer.Helpers;

public interface IUserContextService
{
    string? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    Task<UserInfo<int>?> GetUserInfoAsync();
    Task AddAuthenticatedUserToContext(TokenValidatedContext context);
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserInfoRepository _userInfoRepository;

    public UserContextService(IHttpContextAccessor httpContextAccessor, IUserInfoRepository userInfoRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userInfoRepository = userInfoRepository;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public IEnumerable<string> Roles =>
        _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value) ??
        Enumerable.Empty<string>();

    public async Task<UserInfo<int>?> GetUserInfoAsync()
    {
        if (!IsAuthenticated || UserId == null)
            return null;

        if (_httpContextAccessor.HttpContext?.Items.TryGetValue("User", out var userObj) == true &&
            userObj is UserInfo<int> cachedUser) return cachedUser;

        return await _userInfoRepository.GetByIdAsync(int.Parse(UserId));
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

            var userIdClaim = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
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

            var user = await _userInfoRepository.GetByIdAsync(userId);
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
                new(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString(CultureInfo.InvariantCulture)),
                new(ClaimTypes.Name, user.FullName)
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