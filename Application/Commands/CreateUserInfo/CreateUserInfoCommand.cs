using Domain.Core.Entities;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserInfo;

public record CreateUserInfoCommand(string FullName, int Age, string Email, string PhoneNumber) : ICommand<UserInfo>;