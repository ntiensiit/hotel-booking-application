using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Service<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;

    public virtual bool IsAvailable { get; set; }

    // Value Objects
    public virtual ServiceCategory Category { get; set; }

    public virtual Money Price { get; set; }

    // Reference Ids
    public virtual TId HotelId { get; set; } = default!;

    // Id
    public virtual TId Id { get; set; } = default!;
}

public class Service : Service<int>
{
}

public partial class Service<TId>
{
}