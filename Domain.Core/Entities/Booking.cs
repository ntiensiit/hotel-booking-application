using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Booking : Entity<int>
{
    // Primitive properties
    public virtual DateOnly CheckInDate { get; set; }
    public virtual DateOnly CheckOutDate { get; set; }

    public virtual int NumberOfGuests { get; set; }

    // Enum properties
    public virtual BookingStatus Status { get; set; }

    // Value Objects
    public virtual Money TotalAmount { get; set; }

    public virtual IEnumerable<SpecialRequest> SpecialRequests { get; set; } = [];

    // Reference Ids
    public virtual int UserId { get; set; }
    public virtual int HotelId { get; set; }

    public virtual int RoomId { get; set; }
}

public partial class Booking
{
    public virtual Money CalculateTotal()
    {
        return new Money(250.00m, Currency.Usd);
    }
}
