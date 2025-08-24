using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.DeleteCommands.DeleteHotel;

public class DeleteHotelByIdCommandHandlerV1 : ICommandHandler<DeleteHotelByIdCommandV1>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHotelByIdCommandHandlerV1(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteHotelByIdCommandV1 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var hotel = await _hotelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (hotel == null)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception($"Hotel with ID {request.Id} not found.");
        }

        _hotelRepository.Remove(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
    }
}