namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.User;

public record UserLogoutRequestBody
{
    public string RefreshToken { get; set; }
}