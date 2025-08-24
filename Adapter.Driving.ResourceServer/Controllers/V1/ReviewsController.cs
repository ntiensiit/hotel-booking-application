using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateReview;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Review;
using Adapter.Driving.ResourceServer.Queries.V1.GetReviewsByHotelId;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public ReviewsController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpGet("/hotels/{hotelId:int}/reviews")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
    {
        var query = new GetReviewsByHotelIdQueryV1(hotelId);

        var result =
            await _applicationMediator.SendQueryAsync<GetReviewsByHotelIdQueryV1, IEnumerable<Review<int>>>(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateRequestBodyV1 requestBody)
    {
        var command = new CreateReviewCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateReviewCommandV1, object>(command);

        return Ok(result);
    }
}