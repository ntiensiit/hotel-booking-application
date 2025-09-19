using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Service;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class ServicesController(IApplicationMediator applicationMediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceCreateRequestBodyV1 requestBody)
    {
        var command = new CreateServiceCommandV1(requestBody);

        var result = await applicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
