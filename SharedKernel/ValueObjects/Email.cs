using SharedKernel.SeedWork;

namespace SharedKernel.ValueObjects;

public readonly partial record struct Email : IValueObject
{
    public readonly string Value = Value;
}

public readonly partial record struct Email(string Value);

public readonly partial record struct Email
{
    public static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string email) => new(email);
}
