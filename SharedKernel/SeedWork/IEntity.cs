namespace SharedKernel.SeedWork;

public interface IEntity;

public interface IEntity<out TId> : IEntity
    where TId : IEquatable<TId>, IComparable<TId>
{
    TId Id { get; }
}

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    int Version { get; set; }
}
