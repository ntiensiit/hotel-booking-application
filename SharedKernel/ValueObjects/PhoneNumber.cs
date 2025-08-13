using SharedKernel.SeedWork;

namespace SharedKernel.ValueObjects;

public readonly partial record struct PhoneNumber : IValueObject
{
    public readonly string CountryCode = CountryCode;
    public readonly string Number = Number;
}

public readonly partial record struct PhoneNumber(string CountryCode, string Number);

public readonly partial record struct PhoneNumber
{
    public PhoneNumber(string number) : this(string.Empty, number)
    {
    }
}

public readonly partial record struct PhoneNumber
{
    private static bool IsValid(string countryCode, string number)
    {
        return true;
    }

    public static bool IsValid(PhoneNumber phoneNumber)
    {
        return IsValid(phoneNumber.CountryCode, phoneNumber.Number);
    }

    public static implicit operator string(PhoneNumber phoneNumber)
    {
        return phoneNumber.Number;
    }

    public static implicit operator PhoneNumber(string phoneNumber)
    {
        return new PhoneNumber(phoneNumber);
    }
}