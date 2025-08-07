using Adapter.Driven.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Adapter.Driven.NHibernate.Helpers;

public static class NHibernateHelper
{
    private static ISessionFactory? _sessionFactory;
    private static string? _connectionString;

    public static void SetConnectionString(string connectionString)
    {
        _connectionString = connectionString;
    }

    private static ISessionFactory GetSessionFactory()
    {
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("No connection string configured");
            
        if (_sessionFactory != null) return _sessionFactory;

        var config = Fluently.Configure()
            .Database(
                MsSqlConfiguration.MsSql2012
                    .ConnectionString(_connectionString)
                    .ShowSql()
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
            .BuildConfiguration();

        // new SchemaExport(config).Create(false, true); // (writeToConsole, execute)
        new SchemaExport(config).Execute(false,  true, false);

        _sessionFactory = config.BuildSessionFactory();

        return _sessionFactory;
    }

    public static ISession OpenSession()
    {
        return GetSessionFactory().OpenSession();
    }
}