using Domain.Identity.Entities;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserPrincipal;

public record CreateUserPrincipalCommand(
    int Id,
    bool IsActive,
    DateTime LastLogin,
    string UserName,
    string Email,
    string Password,
    string PhoneNumber) : ICommand<UserPrincipal>;