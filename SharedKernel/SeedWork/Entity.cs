namespace SharedKernel.SeedWork;

public interface IEntity<out TId> where TId : IEquatable<TId>
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