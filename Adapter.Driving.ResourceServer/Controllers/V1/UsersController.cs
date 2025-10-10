using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
public class UsersController : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateRequestBodyV1 requestBody)
    {
        var command = new CreateUserCommandV1(requestBody);

        var result = await _parameter.ApplicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
