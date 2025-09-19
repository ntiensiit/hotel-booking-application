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
        ArgumentException.ThrowIfNullOrEmpty(_connectionString);

        if (_sessionFactory != null)
            return _sessionFactory;

        var config = Fluently
            .Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString).ShowSql())
            .Mappings(m =>
            {
                m.FluentMappings.Add<UserInfoMap>();
                m.FluentMappings.Add<BookingMap>();
                m.FluentMappings.Add<HotelMap>();
                m.FluentMappings.Add<PhotoMap>();
                m.FluentMappings.Add<ReviewMap>();
                m.FluentMappings.Add<RoomMap>();
                m.FluentMappings.Add<ServiceMap>();
            })
            .BuildConfiguration();

        // new SchemaExport(config).Create(false, true); // (writeToConsole, execute)
        new SchemaExport(config).Execute(false, true, false);

        // var cfg = new Configuration();
        // cfg.DataBaseIntegration(db =>
        // {
        //     db.ConnectionString = _connectionString;
        //     db.Dialect<MsSql2012Dialect>();
        //     db.Driver<SqlClientDriver>();
        //     db.LogSqlInConsole = true;
        //     db.Timeout = 30;
        // });
        //
        // cfg.AddAssembly(typeof(UserInfo).Assembly);

        _sessionFactory = config.BuildSessionFactory();

        new DataSeeder(_sessionFactory).SeedUserInfoData();

        return _sessionFactory;
    }

    public static ISession OpenSession()
    {
        return GetSessionFactory().OpenSession();
    }
}
