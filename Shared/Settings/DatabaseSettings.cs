namespace Shared.Settings;

public record DatabaseSettings
{
    public const string SectionName = "ConnectionStrings";

    public string? ResourceServerDBConnection { get; set; }
    public string? AuthenticationServerDBConnection { get; set; }
}
