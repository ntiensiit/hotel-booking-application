using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Repositories;
using Domain.Core.ValueObjects;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.Shared.Events;

namespace Application.Commands.V1.CreateCommands.CreateBooking;

public class CreateBookingCommandHandlerV1 : ICommandHandler<CreateBookingCommandV1, object>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;

    public CreateBookingCommandHandlerV1(IBookingRepository bookingRepository,
        INhibernateUnitOfWork nhibernateUnitOfWork)
    {
        _bookingRepository = bookingRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
    }

    public async Task<object> Handle(CreateBookingCommandV1 request, CancellationToken cancellationToken)
    {
        const BookingStatus bookingStatus = BookingStatus.Pending;
        var totalAmount = new Money(0.0m, Currency.Usd);
        var booking = new Booking<int>(
            request.RequestBody.CheckInDate,
            request.RequestBody.CheckOutDate,
            request.RequestBody.NumberOfGuests,
            bookingStatus,
            totalAmount,
            0,
            request.RequestBody.HotelId,
            request.RequestBody.RoomId);

        await _nhibernateUnitOfWork.BeginTransactionAsync(cancellationToken);
        _bookingRepository.Add(booking);
        await _nhibernateUnitOfWork.SaveChangesAsync(cancellationToken);
        await _nhibernateUnitOfWork.CommitTransactionAsync(cancellationToken);

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