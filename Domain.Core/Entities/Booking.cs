using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Booking<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual DateOnly CheckInDate { get; set; }
    public virtual DateOnly CheckOutDate { get; set; }

    public virtual int NumberOfGuests { get; set; }

    // Enum properties
    public virtual BookingStatus Status { get; set; }

    // Value Objects
    public virtual Money TotalAmount { get; set; }

    public virtual IEnumerable<SpecialRequest> SpecialRequests { get; set; } = new List<SpecialRequest>();

    // Reference Ids
    public virtual TId UserId { get; set; }
    public virtual TId HotelId { get; set; }

    public virtual TId RoomId { get; set; }

    // Id
    public virtual TId Id { get; set; } = default!;
}

public partial class Booking<TId>
{
    public virtual Money CalculateTotal()
    {
        return new Money(250.00m, Currency.Usd);
    }
}