using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public class User : IEntity<int>
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public int Id { get; set; }
}