using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Room : Entity<int>
{
    // Primitive properties
    public virtual string RoomNumber { get; set; }

    public virtual int Capacity { get; set; }

    // Enum properties
    public virtual RoomType RoomType { get; set; }

    public virtual RoomStatus Status { get; set; }

    // Value Objects
    public virtual Pricing Pricing { get; set; }

    public virtual IEnumerable<Amenity> Amenities { get; protected set; } = [];

    // Reference Ids
    public virtual int HotelId { get; set; }
}

public partial class Room
{
    public virtual void SetPricing(Pricing pricing) => Pricing = pricing;

    public virtual void SetAvailability() { }
}
