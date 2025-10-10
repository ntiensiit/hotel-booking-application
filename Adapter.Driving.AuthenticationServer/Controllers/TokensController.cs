using Adapter.Driving.AuthenticationServer.DTOs.Requests.Token;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]/[action]")]
public class TokensController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequestBody requestBody)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        var (accessToken, refreshToken) = await _parameter.JwtTokenService.RefreshTokenAsync(requestBody.RefreshToken, null, ipAddress);

        return Ok(
            new
            {
                Message = "Refresh token successfully.",
                Token = new { AccessToken = accessToken, RefreshToken = refreshToken },
            }
        );
    }
}
