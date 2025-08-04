namespace SharedKernel.SeedWork;

public interface IEntity<TId>
{
    TId Id { get; set; }
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