namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Role;

public record RoleCreateRequestBody
{
    public string Name { get; init; }
}