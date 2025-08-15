using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.User;

namespace Application.Commands.V1.CreateCommands.CreateUser;

public record CreateUserCommandV1(UserCreateRequestBodyV1 RequestBody) : ICommand<object>;