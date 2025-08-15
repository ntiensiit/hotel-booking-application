using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Hotel<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
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
    public virtual TId HostId { get; set; } = default!;

    // Id
    public virtual TId Id { get; set; } = default!;
}

public partial class Hotel<TId>
{
}

public partial class Hotel<TId>
{
    public Hotel()
    {
    }

    public Hotel(string name, string description, DateTime registrationDate, DateTime? approvedDate,
        HotelType hotelType,
        HotelStatus status, Address address, StarRating starRating, ContactInfo contactInfo,
        HotelPolicies hotelPolicies, TId hostId)
    {
        Name = name;
        Description = description;
        RegistrationDate = registrationDate;
        ApprovedDate = approvedDate;
        HotelType = hotelType;
        Status = status;
        Address = address;
        StarRating = starRating;
        ContactInfo = contactInfo;
        HotelPolicies = hotelPolicies;
        HostId = hostId;
    }
}