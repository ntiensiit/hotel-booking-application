namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;

public record AddClaimToUserRequestBody
{
    public int UserId { get; init; }
    public string ClaimType { get; init; }
    public string ClaimValue { get; init; }
}
