using Port.Driving.Shared.DTOs.Requests;
using Port.Driving.Shared.DTOs.Responses;
using Port.Driving.Shared.Services;

namespace Application.Services;

public class AuthService : IAuthService
{
    public async Task<TokenResponseDto> LoginUser(LoginRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<TokenResponseDto> RefreshToken(RefreshTokenRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<LogoutResponseDto> LogoutUser(LogoutRequestDto dto)
    {
        throw new NotImplementedException();
    }
}