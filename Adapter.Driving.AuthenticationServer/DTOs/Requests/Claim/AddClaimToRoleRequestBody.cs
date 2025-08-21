namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;

public record AddClaimToRoleRequestBody
{
    public int RoleId { get; init; }
    public string ClaimType { get; init; }
    public string ClaimValue { get; init; }
}