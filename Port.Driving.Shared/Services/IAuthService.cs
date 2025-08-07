using Port.Driving.Shared.DTOs.Requests;
using Port.Driving.Shared.DTOs.Responses;

namespace Port.Driving.Shared.Services;

public interface IAuthService
{
    Task<TokenResponseDto> LoginUser(LoginRequestDto dto);
    Task<TokenResponseDto> RefreshToken(RefreshTokenRequestDto dto);
    Task<LogoutResponseDto> LogoutUser(LogoutRequestDto dto);
}