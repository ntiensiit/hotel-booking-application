using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Service;

namespace Application.Commands.V1.CreateCommands.CreateService;

public record CreateServiceCommandV1(ServiceCreateRequestBodyV1 RequestBody) : ICommand<object>;