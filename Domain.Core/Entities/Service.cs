using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Service : Entity<int>
{
    // Primitive properties
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }

    public virtual bool IsAvailable { get; set; }

    // Value Objects
    public virtual ServiceCategory Category { get; set; }

    public virtual Money Price { get; set; }

    // Reference Ids
    public virtual int HotelId { get; set; }
}

public partial class Service { }
