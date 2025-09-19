using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Review : IEntity<int>
{
    // Primitive properties
    public virtual bool IsVerified { get; set; }

    public virtual string Comment { get; set; }

    // Value Objects
    public virtual Rating Rating { get; set; }

    public virtual IEnumerable<Photo> Photos { get; set; } = [];

    // Reference Ids
    public virtual int UserId { get; set; }
    public virtual int HotelId { get; set; }

    public virtual int BookingId { get; set; }

    // Id
    public virtual int Id { get; set; }
}

public partial class Review { }
