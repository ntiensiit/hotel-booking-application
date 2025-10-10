namespace SharedKernel.SeedWork;

public interface IEntity<out TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    TId Id { get; }
}

public abstract class Entity<TId> : IEntity<TId>, IAuditable, ISoftDelete
    where TId : IEquatable<TId>, IComparable<TId>
{
    public virtual TId Id { get; protected set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime? UpdatedAt { get; set; }
    public virtual int Version { get; set; }
    public virtual bool IsDeleted { get; set; }
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
