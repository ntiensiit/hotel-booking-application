using Domain.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public partial class UserPrincipal : IdentityUser<int>, IEntity<int>
{
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public UserType UserType { get; set; }
}

public partial class UserPrincipal
{
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void RecordLogin()
    {
        LastLogin = DateTime.UtcNow;
    }
}