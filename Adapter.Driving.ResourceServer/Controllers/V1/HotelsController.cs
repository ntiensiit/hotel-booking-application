using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Hotel;
using Adapter.Driving.ResourceServer.Queries.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
public class HotelsController : BaseController
{
    [HttpGet("paging")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = new GetHotelsByPagingQueryV1(pageNumber, pageSize);

        var result = await _parameter.ApplicationMediator.SendQueryAsync(query);

        return Ok(
            new
            {
                result.Content,
                result.PageNumber,
                result.PageSize,
                result.TotalPages,
                result.TotalElements,
                result.HasNextPage,
                result.HasPreviousPage,
            }
        );
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetHotelByIdQueryV1(id);

        var result = await _parameter.ApplicationMediator.SendQueryAsync(query);

        if (result is null)
            return NoContent();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelCreateRequestBodyV1 requestBody)
    {
        var command = new CreateHotelCommandV1(requestBody);

        var result = await _parameter.ApplicationMediator.SendCommandAsync(command);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteHotelByIdCommandV1(id);
        await _parameter.ApplicationMediator.SendCommandAsync(command);
        return NoContent();
    }
}
