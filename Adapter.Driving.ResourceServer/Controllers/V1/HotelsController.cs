using Application.Commands.V1.CreateCommands.CreateHotel;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Hotel;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public HotelsController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelCreateRequestBodyV1 requestBody)
    {
        var command = new CreateHotelCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateHotelCommandV1, object>(command);

        return Ok(result);
    }
}