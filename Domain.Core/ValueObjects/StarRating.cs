using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct StarRating : IValueObject
{
    public readonly int Value = Value;
}

public readonly partial record struct StarRating(int Value);

public readonly partial record struct StarRating
{
    public static bool IsValid(int value)
    {
        return value is >= 1 and <= 10;
    }
}
