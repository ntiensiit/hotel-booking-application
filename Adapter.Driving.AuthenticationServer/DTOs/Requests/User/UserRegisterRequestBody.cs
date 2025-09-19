namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.User;

public record UserRegisterRequestBody
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public string PasswordConfirmed { get; init; }
}
