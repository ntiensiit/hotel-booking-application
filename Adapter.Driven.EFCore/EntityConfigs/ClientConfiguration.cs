using System.Text;
using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.Driven.EFCore.EntityConfigs;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.HasData(
            new Client
            {
                Id = 1,
                ClientId = "Client1",
                Name = "Client Application 1",
                ClientSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes("this_is_a_very_long_secret_key_secret1")),
                ClientUrl = "https://client1.com",
                IsActive = true
            },
            new Client
            {
                Id = 2,
                ClientId = "Client2",
                Name = "Client Application 2",
                ClientSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes("this_is_a_very_long_secret_key_secret2")),
                ClientUrl = "https://client2.com",
                IsActive = true
            });
    }
}