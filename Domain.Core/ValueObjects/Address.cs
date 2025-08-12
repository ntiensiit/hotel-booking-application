using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct Address : IValueObject
{
    public readonly string City = City;
    public readonly Coordinates Coordinates = Coordinates;
    public readonly string Country = Country;
    public readonly string PostalCode = PostalCode;
    public readonly string State = State;
    public readonly string Street = Street;
}

public readonly partial record struct Address(
    string City,
    Coordinates Coordinates,
    string Country,
    string PostalCode,
    string State,
    string Street);

public readonly partial record struct Address
{
}