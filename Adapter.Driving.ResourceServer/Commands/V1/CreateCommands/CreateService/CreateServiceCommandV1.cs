using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Service;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateService;

public record CreateServiceCommandV1(ServiceCreateRequestBodyV1 RequestBody) : ICommand<object>;