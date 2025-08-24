using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Room;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateRoom;

public record CreateRoomCommandV1(RoomCreateRequestBodyV1 RequestBody) : ICommand<object>;