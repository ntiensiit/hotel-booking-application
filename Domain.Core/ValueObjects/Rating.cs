using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct Rating : IValueObject
{
    public readonly int CleanlinessRating = CleanlinessRating;
    public readonly int LocationRating = LocationRating;
    public readonly int OverallRating = OverallRating;
    public readonly int ServiceRating = ServiceRating;
    public readonly int ValueRating = ValueRating;
}

public readonly partial record struct Rating(
    int CleanlinessRating,
    int LocationRating,
    int OverallRating,
    int ServiceRating,
    int ValueRating
);

public readonly partial record struct Rating
{
    public int AverageRating =>
        (OverallRating + CleanlinessRating + ServiceRating + LocationRating + ValueRating) / 5;
}
