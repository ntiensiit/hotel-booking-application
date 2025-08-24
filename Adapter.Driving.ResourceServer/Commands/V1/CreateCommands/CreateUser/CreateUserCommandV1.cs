using Adapter.Driving.ResourceServer.DTOs.V1.Requests.User;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateUser;

public record CreateUserCommandV1(UserCreateRequestBodyV1 RequestBody) : ICommand<object>;