using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateHotel;
using Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands.DeleteHotel;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Hotel;
using Adapter.Driving.ResourceServer.Queries.V1.GetHotelById;
using Adapter.Driving.ResourceServer.Queries.V1.GetHotelsByPaging;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

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

    [HttpGet("/paging")]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetHotelsByPagingQueryV1(pageNumber, pageSize);

        var result = await _applicationMediator.SendQueryAsync<GetHotelsByPagingQueryV1, IPage<Hotel<int>>>(query);

        return Ok(new
        {
            result.Content,
            result.PageNumber,
            result.PageSize,
            result.TotalPages,
            result.TotalElements,
            result.HasNextPage,
            result.HasPreviousPage
        });
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetHotelByIdQueryV1(id);

        var result = await _applicationMediator.SendQueryAsync<GetHotelByIdQueryV1, object?>(query);

        if (result is null) return NoContent();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelCreateRequestBodyV1 requestBody)
    {
        var command = new CreateHotelCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateHotelCommandV1, object>(command);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteHotelByIdCommandV1(id);
        await _applicationMediator.SendCommandAsync(command);
        return NoContent();
    }
}