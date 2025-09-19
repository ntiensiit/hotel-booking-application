using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Room;
using Adapter.Driving.ResourceServer.Queries.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class RoomsController(IApplicationMediator applicationMediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetRoomByIdQueryV1(id);

        var result = await applicationMediator.SendQueryAsync(query);

        if (result is null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("hotel/{hotelId:int}/rooms")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
    {
        var query = new GetRoomsByHotelIdQueryV1(hotelId);

        var result = await applicationMediator.SendQueryAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomCreateRequestBodyV1 requestBody)
    {
        var command = new CreateRoomCommandV1(requestBody);

        var result = await applicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
