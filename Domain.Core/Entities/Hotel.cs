using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Hotel : IEntity<int>
{
    // Primitive properties
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual DateTime RegistrationDate { get; set; }

    public virtual DateTime? ApprovedDate { get; set; }

    // Enum properties
    public virtual HotelType HotelType { get; set; }

    public virtual HotelStatus Status { get; set; }

    // Value Objects
    public virtual Address Address { get; set; }
    public virtual StarRating StarRating { get; set; }
    public virtual ContactInfo ContactInfo { get; set; }

    public virtual HotelPolicies HotelPolicies { get; set; }

    // Reference Ids
    public virtual int HostId { get; set; }

    // Id
    public virtual int Id { get; set; }
}

public partial class Hotel { }
