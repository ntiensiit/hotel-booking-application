using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct Coordinates : IValueObject
{
    public readonly decimal Latitude = Latitude;
    public readonly decimal Longitude = Longitude;
}

public readonly partial record struct Coordinates(decimal Latitude, decimal Longitude);

public readonly partial record struct Coordinates { }
