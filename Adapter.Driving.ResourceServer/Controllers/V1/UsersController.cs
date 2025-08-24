using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateUser;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public UsersController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateRequestBodyV1 requestBody)
    {
        var command = new CreateUserCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateUserCommandV1, object>(command);

        return Ok(result);
    }
}