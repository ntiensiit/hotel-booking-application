using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserInfo;

public record CreateUserInfoCommand(string FullName, DateTime DateOfBirth, string Email, string PhoneNumber)
    : ICommand<UserInfo>;