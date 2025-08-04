namespace Shared.Settings;

public class DatabaseSetting
{
    public string OracleDbConnectionString { get; init; } = null!;
    public string OracleDbDatabaseName { get; init; } = null!;
    public string EventStoreConnectionString { get; init; } = null!;
}