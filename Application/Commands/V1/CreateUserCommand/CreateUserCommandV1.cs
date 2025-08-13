using System.ComponentModel.DataAnnotations;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests;
using Port.Driving.Shared.DTOs.V1.Responses;

namespace Application.Commands.V1.CreateUserCommand;

public partial record CreateUserCommandV1 : ICommand<UserInfoResponseDtoV1>
{
    [Required] public string FullName { get; init; } = null!;

    [Required] public DateTime DateOfBirth { get; init; }

    [Required] public string Email { get; init; } = null!;

    [Required] public string PhoneNumber { get; init; } = null!;

    [Required] public string UserName { get; init; } = null!;

    [Required] public string Password { get; init; } = null!;

    [Required] public string PasswordConfirmed { get; init; } = null!;
}

public partial record CreateUserCommandV1
{
    public static CreateUserCommandV1 Create(RegisterRequestDtoV1 registerRequestDtoV1)
    {
        return new CreateUserCommandV1
        {
            FullName = registerRequestDtoV1.FullName,
            DateOfBirth = registerRequestDtoV1.DateOfBirth,
            Email = registerRequestDtoV1.Email,
            PhoneNumber = registerRequestDtoV1.PhoneNumber,
            UserName = registerRequestDtoV1.UserName,
            Password = registerRequestDtoV1.Password,
            PasswordConfirmed = registerRequestDtoV1.ConfirmPassword
        };
    }
}