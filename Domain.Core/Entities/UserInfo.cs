using SharedKernel.SeedWork;
using SharedKernel.ValueObjects;

namespace Domain.Core.Entities;

public partial class UserInfo : Entity<int>
{
    // Primitive properties
    public virtual string FullName { get; set; }
    public virtual DateTime DateOfBirth { get; set; }
    public virtual Email Email { get; set; }

    public virtual PhoneNumber PhoneNumber { get; set; }
}

public partial class UserInfo
{
    public virtual void UpdateEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(email));
        Email = email;
    }

    public virtual void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        if (PhoneNumber.IsValid(phoneNumber))
            PhoneNumber = phoneNumber;
    }
}
