using Application.Commands.V1.CreateCommands.CreateReview;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Review;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateRequestBodyV1 requestBody)
    {
        var command = new CreateReviewCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateReviewCommandV1, object>(command);

        return Ok(result);
    }
}