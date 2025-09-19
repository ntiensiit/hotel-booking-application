namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;

public record UserClaimUpdateRequestBody
{
    public string ClaimType { get; init; }
    public string ClaimValue { get; init; }
}
