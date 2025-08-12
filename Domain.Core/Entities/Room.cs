using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Room<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual string RoomNumber { get; set; } = string.Empty;

    public virtual int Capacity { get; set; }

    // Enum properties
    public virtual RoomType RoomType { get; set; }

    public virtual RoomStatus Status { get; set; }

    // Value Objects
    public virtual Pricing Pricing { get; set; }

    public virtual IEnumerable<Amenity> Amenities { get; protected set; } = new List<Amenity>();

    // Reference Ids
    public virtual TId HotelId { get; set; } = default!;

    // Id
    public virtual TId Id { get; set; } = default!;
}

public class Room : Room<int>
{
}

public partial class Room<TId>
{
    public virtual void SetPricing(Pricing pricing)
    {
        Pricing = pricing;
    }

    public virtual void SetAvailability()
    {
    }
}