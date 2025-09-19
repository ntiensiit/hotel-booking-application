using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public class Client : IEntity<int>
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string ClientSecret { get; set; }
    public string ClientUrl { get; set; }
    public bool IsActive { get; set; }

    // Id
    public int Id { get; set; }
}
