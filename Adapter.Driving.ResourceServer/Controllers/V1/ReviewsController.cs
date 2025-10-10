using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Review;
using Adapter.Driving.ResourceServer.Queries.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
public class ReviewsController : BaseController
{
    [HttpGet("hotels/{hotelId:int}/reviews")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
    {
        var query = new GetReviewsByHotelIdQueryV1(hotelId);

        var result = await _parameter.ApplicationMediator.SendQueryAsync(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateRequestBodyV1 requestBody)
    {
        var command = new CreateReviewCommandV1(requestBody);

        var result = await _parameter.ApplicationMediator.SendCommandAsync(command);

        return Ok(result);
    }
}
