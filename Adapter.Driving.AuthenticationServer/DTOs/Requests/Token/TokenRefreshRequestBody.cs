namespace Adapter.Driving.AuthenticationServer.DTOs.Requests.Token;

public record TokenRefreshRequestBody
{
    public string RefreshToken { get; init; }
}