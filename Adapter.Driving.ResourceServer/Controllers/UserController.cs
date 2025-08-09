using Microsoft.AspNetCore.Mvc;
using Port.Driving.Shared.DTOs.Requests;
using Port.Driving.Shared.Services;

namespace Adapter.Driving.ResourceServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequestDto dto)
    {
        var result = await _userService.RegisterNewUser(dto);
        return Ok(result);
    }
}