using Application.Commands.V1.CreateCommands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.User;

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
    public async Task<IActionResult> Create([FromBody] UserCreateRequestBodyV1 requestBody)
    {
        var command = new CreateUserCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateUserCommandV1, object>(command);

        return Ok(result);
    }
}