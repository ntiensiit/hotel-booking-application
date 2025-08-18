using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.ValueObjects;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Application.Commands.V1.CreateCommands.CreateHotel;

public class CreateHotelCommandHandlerV1 : ICommandHandler<CreateHotelCommandV1, object>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;

    public CreateHotelCommandHandlerV1(IHotelRepository hotelRepository, INhibernateUnitOfWork nhibernateUnitOfWork)
    {
        _hotelRepository = hotelRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
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
        var hotel = new Hotel<int>(request.RequestBody.Name, request.RequestBody.Description, DateTime.UtcNow, null,
            hotelType, status, address,
            starRating, contactInfo, hotelPolicies, hostId);

        await _nhibernateUnitOfWork.BeginTransactionAsync(cancellationToken);
        _hotelRepository.Add(hotel);
        await _nhibernateUnitOfWork.SaveChangesAsync(cancellationToken);
        await _nhibernateUnitOfWork.CommitTransactionAsync(cancellationToken);

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