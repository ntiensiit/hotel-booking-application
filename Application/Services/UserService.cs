using Application.Commands.CreateUserInfo;
using Application.Commands.CreateUserPrincipal;
using Domain.Core.Entities;
using Domain.Identity.Entities;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.Requests;
using Port.Driving.Shared.DTOs.Responses;
using Port.Driving.Shared.Services;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationMediator _applicationMediator;

    public UserService(IApplicationMediator applicationMediator)
    {
        _applicationMediator = applicationMediator;
    }

    public async Task RegisterNewUser(RegisterRequestDto dto)
    {
        var createUserInfoCommand = new CreateUserInfoCommand
        (
            dto.FullName,
            dto.DateOfBirth,
            dto.Email,
            dto.PhoneNumber
        );

        var newUserInfo =
            await _applicationMediator.SendCommandAsync<CreateUserInfoCommand, UserInfo>(createUserInfoCommand);

        var createUserPrincipalCommand = new CreateUserPrincipalCommand
        (newUserInfo.Id, true, DateTime.UtcNow, "test", newUserInfo.Email, dto.Password,
            newUserInfo.PhoneNumber.Number);

        var newUserPrincipal =
            await _applicationMediator.SendCommandAsync<CreateUserPrincipalCommand, UserPrincipal>(
                createUserPrincipalCommand);

        var tokenResponseDto = new TokenResponseDto("test", "");
        var userInfoResponseDto = new UserInfoResponseDto(newUserInfo.FullName, newUserInfo.DateOfBirth,
            newUserInfo.Email,
            newUserInfo.PhoneNumber.Number);
    }

    public async Task<UserInfoResponseDto> GetCurrentUserInfo()
    {
        throw new NotImplementedException();
    }
}