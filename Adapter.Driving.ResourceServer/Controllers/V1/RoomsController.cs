using Application.Commands.V1.CreateCommands.CreateRoom;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Room;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public RoomsController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomCreateRequestBodyV1 requestBody)
    {
        var command = new CreateRoomCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateRoomCommandV1, object>(command);
        
        return Ok(result);
    }
}