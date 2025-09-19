using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands;

public record DeleteHotelByIdCommandV1(int Id) : ICommand;

public class DeleteHotelByIdCommandHandlerV1(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<DeleteHotelByIdCommandV1>
{
    public async Task HandleAsync(DeleteHotelByIdCommandV1 request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var hotel = await hotelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (hotel == null)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new KeyNotFoundException($"Hotel with ID {request.Id} not found.");
        }

        hotelRepository.Remove(hotel);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);
    }
}
