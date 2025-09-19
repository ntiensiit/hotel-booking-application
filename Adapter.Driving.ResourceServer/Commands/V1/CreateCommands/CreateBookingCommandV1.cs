using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Booking;
using Adapter.Driving.ResourceServer.Helpers;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;

public record CreateBookingCommandV1(BookingCreateRequestBodyV1 RequestBody) : ICommand<object>;

public class CreateBookingCommandHandlerV1(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IHttpUserContextService userContextService
) : ICommandHandler<CreateBookingCommandV1, object>
{
    public async Task<object> HandleAsync(CreateBookingCommandV1 request, CancellationToken cancellationToken)
    {
        if (!int.TryParse(userContextService.UserId, out var userId))
            throw new FormatException($"Invalid User ID {userContextService.UserId}");

        const BookingStatus bookingStatus = BookingStatus.Pending;
        var totalAmount = new Money(0.0m, Currency.Usd);
        var booking = new Booking
        {
            CheckInDate = request.RequestBody.CheckInDate,
            CheckOutDate = request.RequestBody.CheckOutDate,
            NumberOfGuests = request.RequestBody.NumberOfGuests,
            Status = bookingStatus,
            TotalAmount = totalAmount,
            UserId = userId,
            HotelId = request.RequestBody.HotelId,
            RoomId = request.RequestBody.RoomId,
        };

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        bookingRepository.Add(booking);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        return new
        {
            Booking = new
            {
                booking.CheckInDate,
                booking.CheckOutDate,
                booking.NumberOfGuests,
                booking.Status,
                Total = totalAmount,
                booking.HotelId,
                booking.RoomId,
                booking.Id,
            },
        };
    }
}
