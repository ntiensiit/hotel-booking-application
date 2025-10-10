using Adapter.Driving.AuthenticationServer.DTOs.Requests.User;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]/[action]")]
public class AccountsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestBody requestBody)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (requestBody.Password != requestBody.PasswordConfirmed)
            return BadRequest("Password and confirmation password do not match.");

        if (await _parameter.UserManager.FindByEmailAsync(requestBody.Email) != null)
            return BadRequest("Email already exists.");

        var user = new ApplicationUser
        {
            UserName = requestBody.UserName,
            Email = requestBody.Email,
            PhoneNumber = requestBody.PhoneNumber,
        };

        var result = await _parameter.UserManager.CreateAsync(user, requestBody.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _parameter.RoleManager.RoleExistsAsync("Customer"))
            await _parameter.RoleManager.CreateAsync(new ApplicationRole { Name = "Customer" });

        await _parameter.UserManager.AddToRoleAsync(user, "Customer");

        return Ok(
            new
            {
                Message = "User registered successfully",
                User = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                },
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestBody requestBody)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _parameter.UserManager.FindByEmailAsync(requestBody.Email);

        if (user == null)
            return NotFound("Invalid credentials");

        var result = await _parameter.SignInManager.CheckPasswordSignInAsync(user, requestBody.Password, false);

        if (!result.Succeeded)
            return Unauthorized("Invalid credentials");

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        var roles = await _parameter.UserManager.GetRolesAsync(user);

        var (accessToken, refreshToken) = await _parameter.JwtTokenService.GenerateLoginTokenAsync(user.Id.ToString(), user.Email!, [.. roles], ipAddress);

        return Ok(
            new
            {
                Token = new { AccessToken = accessToken, RefreshToken = refreshToken },
                UserDetails = new
                {
                    UserId = user.Id,
                    user.Email,
                    user.UserName,
                },
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Logout([FromBody] UserLogoutRequestBody requestBody)
    {
        await _parameter.JwtTokenService.RevokeRefreshTokenAsync(requestBody.RefreshToken);

        return Ok(new { Message = "Logout successfully." });
    }
}
