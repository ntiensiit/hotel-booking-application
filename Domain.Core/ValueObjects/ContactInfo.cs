using SharedKernel.SeedWork;
using SharedKernel.ValueObjects;

namespace Domain.Core.ValueObjects;

public readonly partial record struct ContactInfo : IValueObject
{
    public readonly Email Email = Email;
    public readonly PhoneNumber Phone = Phone;
    public readonly string Website = Website;
}

public readonly partial record struct ContactInfo(Email Email, PhoneNumber Phone, string Website);