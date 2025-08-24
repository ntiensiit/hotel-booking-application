using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateService;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Service;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public ServicesController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceCreateRequestBodyV1 requestBody)
    {
        var command = new CreateServiceCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateServiceCommandV1, object>(command);

        return Ok(result);
    }
}