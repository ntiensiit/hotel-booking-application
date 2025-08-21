namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.User;

public record UserLoginRequestBody
{
    public string Email { get; init; }
    public string Password { get; init; }
}