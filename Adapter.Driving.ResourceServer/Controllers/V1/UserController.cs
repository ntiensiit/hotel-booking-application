using Application.Commands.V1.CreateUserCommand;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests;
using Port.Driving.Shared.DTOs.V1.Responses;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public UserController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDtoV1 dtoV1)
    {
        var createUserCommand = CreateUserCommandV1.Create(dtoV1);

        var userInfoResponse =
            await _applicationMediator.SendCommandAsync<CreateUserCommandV1, UserInfoResponseDtoV1>(createUserCommand);

        var result = new RegisterResponseDtoV1(userInfoResponse);

        return Ok(result);
    }
}