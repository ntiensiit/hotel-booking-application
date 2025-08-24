using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Booking;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateBooking;

public record CreateBookingCommandV1(BookingCreateRequestBodyV1 RequestBody) : ICommand<object>;