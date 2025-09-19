using Adapter.Driving.AuthenticationServer.DTOs.Requests.Token;
using Adapter.Driving.AuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TokensController(IJwtTokenService jwtTokenService) : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequestBody requestBody)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        var (accessToken, refreshToken) = await _jwtTokenService.RefreshTokenAsync(
            requestBody.RefreshToken,
            null,
            ipAddress
        );

        return Ok(
            new
            {
                Message = "Refresh token successfully.",
                Token = new { AccessToken = accessToken, RefreshToken = refreshToken },
            }
        );
    }
}
