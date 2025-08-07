using SharedKernel.SeedWork;

namespace Domain.Core.Entities;

public class UserInfo : IEntity<int>
{
    public virtual string FullName { get; set; } = string.Empty;
    public virtual int Age { get; set; }
    public virtual string Email { get; set; } = string.Empty;
    public virtual string PhoneNumber { get; set; } = string.Empty;

    public virtual int Id { get; set; }

    public virtual void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

        Email = email;
    }
}