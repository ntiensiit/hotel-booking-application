using SharedKernel.SeedWork;

namespace Domain.Identity.Entities;

public class RefreshToken : IEntity<int>
{
    public int Id { get; set; }
}