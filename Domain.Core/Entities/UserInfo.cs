using SharedKernel.SeedWork;
using SharedKernel.ValueObjects;

namespace Domain.Core.Entities;

public partial class UserInfo<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    // Primitive properties
    public virtual string FullName { get; set; } = string.Empty;
    public virtual DateTime DateOfBirth { get; set; }
    public virtual Email Email { get; set; }

    public virtual PhoneNumber PhoneNumber { get; set; }

    // Id
    public virtual TId Id { get; set; } = default!;
}

public partial class UserInfo<TId>
{
    public virtual void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

        Email = email;
    }

    public virtual void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        if (PhoneNumber.IsValid(phoneNumber)) PhoneNumber = phoneNumber;
    }
}

public partial class UserInfo<TId>
{
    public UserInfo()
    {
    }

    public UserInfo(string fullName, DateTime dateOfBirth, string email, PhoneNumber phoneNumber)
    {
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}