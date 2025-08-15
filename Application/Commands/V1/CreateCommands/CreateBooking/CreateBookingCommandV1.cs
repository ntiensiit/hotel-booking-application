using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Booking;

namespace Application.Commands.V1.CreateCommands.CreateBooking;

public record CreateBookingCommandV1(BookingCreateRequestBodyV1 RequestBody) : ICommand<object>;