using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Review<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual bool IsVerified { get; set; }

    public virtual string Comment { get; set; } = string.Empty;

    // Value Objects
    public virtual Rating Rating { get; set; }

    public virtual IEnumerable<Photo> Photos { get; set; } = Array.Empty<Photo>();

    // Reference Ids
    public virtual TId UserId { get; set; } = default!;
    public virtual TId HotelId { get; set; } = default!;

    public virtual TId BookingId { get; set; } = default!;

    // Id
    public virtual TId Id { get; set; } = default!;
}

public class Review : Review<int>
{
}

public partial class Review<TId>
{
}