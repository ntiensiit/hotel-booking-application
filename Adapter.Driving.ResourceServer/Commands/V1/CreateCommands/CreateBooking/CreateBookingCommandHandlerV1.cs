using Adapter.Driving.ResourceServer.Helpers;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateBooking;

public class CreateBookingCommandHandlerV1 : ICommandHandler<CreateBookingCommandV1, object>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContextService;

    public CreateBookingCommandHandlerV1(IBookingRepository bookingRepository, IUnitOfWork unitOfWork,
        IUserContextService userContextService)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _userContextService = userContextService;
    }

    public async Task<object> Handle(CreateBookingCommandV1 request, CancellationToken cancellationToken)
    {
        if (!int.TryParse(_userContextService.UserId, out var userId)) throw new Exception("Invalid User ID");

        const BookingStatus bookingStatus = BookingStatus.Pending;
        var totalAmount = new Money(0.0m, Currency.Usd);
        var booking = new Booking<int>
        {
            CheckInDate = request.RequestBody.CheckInDate,
            CheckOutDate = request.RequestBody.CheckOutDate,
            NumberOfGuests = request.RequestBody.NumberOfGuests,
            Status = bookingStatus,
            TotalAmount = totalAmount,
            UserId = userId,
            HotelId = request.RequestBody.HotelId,
            RoomId = request.RequestBody.RoomId
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        _bookingRepository.Add(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

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
                booking.Id
            }
        };
    }
}