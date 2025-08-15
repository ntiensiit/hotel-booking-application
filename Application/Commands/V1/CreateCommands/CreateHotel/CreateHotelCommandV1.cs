using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Requests.Hotel;

namespace Application.Commands.V1.CreateCommands.CreateHotel;

public record CreateHotelCommandV1(HotelCreateRequestBodyV1 RequestBody) : ICommand<object>;