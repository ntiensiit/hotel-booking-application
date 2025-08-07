using Microsoft.AspNetCore.Identity;
using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public class UserPrincipal : IdentityUser<int>, IEntity<int>
{
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }

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