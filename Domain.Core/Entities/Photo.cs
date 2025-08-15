using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public partial class Photo<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Id
    public virtual TId Id { get; set; } = default!;
}

public partial class Photo<TId>
{
}