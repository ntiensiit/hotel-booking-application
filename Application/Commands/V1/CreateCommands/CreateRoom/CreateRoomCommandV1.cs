using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Room;

namespace Application.Commands.V1.CreateCommands.CreateRoom;

public record CreateRoomCommandV1(RoomCreateRequestBodyV1 RequestBody) : ICommand<object>;