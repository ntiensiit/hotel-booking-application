using Port.Driven.Shared.Events;

namespace Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands.DeleteHotel;

public record DeleteHotelByIdCommandV1(int Id) : ICommand;