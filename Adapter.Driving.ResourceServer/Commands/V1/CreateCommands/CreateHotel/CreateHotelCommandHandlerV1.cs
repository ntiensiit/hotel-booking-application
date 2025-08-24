using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateHotel;

public class CreateHotelCommandHandlerV1 : ICommandHandler<CreateHotelCommandV1, object>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHotelCommandHandlerV1(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CreateHotelCommandV1 request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.RequestBody.HotelType.ToString(), true, out HotelType hotelType))
            throw new ArgumentException(
                $"Invalid HotelType value: '{request.RequestBody.HotelType}'. " +
                $"Valid values are: {string.Join(", ", Enum.GetNames(typeof(HotelType)))}");
        const HotelStatus status = HotelStatus.Pending;
        var address = new Address(request.RequestBody.Address.City,
            new Coordinates(request.RequestBody.Address.Latitude,
                request.RequestBody.Address.Longitude),
            request.RequestBody.Address.Country,
            request.RequestBody.Address.PostalCode,
            request.RequestBody.Address.State,
            request.RequestBody.Address.Street);
        var starRating = new StarRating(request.RequestBody.StarRating);
        var contactInfo = new ContactInfo(request.RequestBody.ContactInfo.Email,
            request.RequestBody.ContactInfo.Phone,
            request.RequestBody.ContactInfo.Website);
        var hotelPolicies = new HotelPolicies();
        var hostId = request.RequestBody.HostId;
        var hotel = new Hotel<int>
        {
            Name = request.RequestBody.Name,
            Description = request.RequestBody.Description,
            RegistrationDate = DateTime.UtcNow,
            ApprovedDate = null,
            HotelType = hotelType,
            Status = status, Address = address,
            StarRating = starRating,
            ContactInfo = contactInfo,
            HotelPolicies = hotelPolicies,
            HostId = hostId
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        _hotelRepository.Add(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

        return new
        {
            Hotel = new
            {
                hotel.Name,
                hotel.Description,
                hotel.RegistrationDate,
                hotel.HotelType,
                hotel.Status,
                hotel.Address,
                hotel.StarRating,
                hotel.ContactInfo,
                hotel.HotelPolicies,
                hotel.HostId,
                hotel.Id
            }
        };
    }
}