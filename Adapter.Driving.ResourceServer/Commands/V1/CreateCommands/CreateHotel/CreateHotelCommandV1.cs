using Adapter.Driving.ResourceServer.DTOs.V1.Requests.Hotel;
using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateHotel;

public record CreateHotelCommandV1(HotelCreateRequestBodyV1 RequestBody) : ICommand<object>;