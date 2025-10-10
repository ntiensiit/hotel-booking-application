using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Service;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
public class ServicesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceCreateRequestBodyV1 requestBody)
    {
        var command = new CreateServiceCommandV1(requestBody);

        var result = await _parameter.ApplicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
