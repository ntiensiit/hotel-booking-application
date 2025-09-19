using Domain.Core.Entities;
using NHibernate;

namespace Adapter.Driven.NHibernate.Helpers;

internal class DataSeeder(ISessionFactory sessionFactory)
{
    private readonly ISessionFactory _sessionFactory = sessionFactory;

    public DataSeeder SeedUserInfoData()
    {
        using var session = _sessionFactory.OpenSession();
        using var transaction = session.BeginTransaction();

        try
        {
            var existingAdmin = session.Query<UserInfo>().Any(u => u.Email == "admin@example.com");
            var existingCustomer = session
                .Query<UserInfo>()
                .Any(u => u.Email == "customer@example.com");
            var existingHost = session.Query<UserInfo>().Any(u => u.Email == "host@example.com");

            if (!existingAdmin)
            {
                var admin = new UserInfo
                {
                    FullName = "Admin",
                    DateOfBirth = DateTime.UtcNow,
                    Email = "admin@example.com",
                    PhoneNumber = "",
                };
                session.Save(admin);
            }

            if (!existingCustomer)
            {
                var customer = new UserInfo
                {
                    FullName = "Customer",
                    DateOfBirth = DateTime.UtcNow,
                    Email = "customer@example.com",
                    PhoneNumber = "",
                };
                session.Save(customer);
            }

            if (!existingHost)
            {
                var host = new UserInfo
                {
                    FullName = "Host",
                    DateOfBirth = DateTime.UtcNow,
                    Email = "host@example.com",
                    PhoneNumber = "",
                };
                session.Save(host);
            }

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.Message);
            throw;
        }

        return this;
    }
}
