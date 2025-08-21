namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;

public record RoleClaimUpdateRequestBody
{
    public string ClaimType { get; init; }
    public string ClaimValue { get; init; }
}