using Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateBooking;
using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Controllers.V1;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IApplicationMediator _applicationMediator;

    public BookingController(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] BookingCreateRequestBodyV1 requestBody)
    {
        var command = new CreateBookingCommandV1(requestBody);

        var result = await _applicationMediator.SendCommandAsync<CreateBookingCommandV1, object>(command);

        return Ok(result);
    }
}