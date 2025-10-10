using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Room;
using Adapter.Driving.ResourceServer.Queries.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
public class RoomsController : BaseController
{
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetRoomByIdQueryV1(id);

        var result = await _parameter.ApplicationMediator.SendQueryAsync(query);

        if (result is null)
            return NoContent();

        return Ok(result);
    }

    [HttpGet("hotel/{hotelId:int}/rooms")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
    {
        var query = new GetRoomsByHotelIdQueryV1(hotelId);

        var result = await _parameter.ApplicationMediator.SendQueryAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomCreateRequestBodyV1 requestBody)
    {
        var command = new CreateRoomCommandV1(requestBody);

        var result = await _parameter.ApplicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
