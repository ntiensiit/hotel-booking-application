namespace Shared.Settings;

public record IdentitySettings
{
    public const string SectionName = "Identity";

    public bool RequireDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public int RequiredUniqueChars { get; set; }
    public bool RequireUniqueEmail { get; set; }
}