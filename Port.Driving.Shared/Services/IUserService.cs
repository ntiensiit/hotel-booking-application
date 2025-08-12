using Port.Driving.Shared.DTOs.Requests;
using Port.Driving.Shared.DTOs.Responses;

namespace Port.Driving.Shared.Services;

public interface IUserService
{
    Task RegisterNewUser(RegisterRequestDto dto);
    Task<UserInfoResponseDto> GetCurrentUserInfo();
}