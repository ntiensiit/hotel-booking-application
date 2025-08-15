namespace Port.Driving.Shared.DTOs.V1.Requests.Hotel;

public record HotelCreateRequestBodyV1
{
    public enum HotelTypeDto
    {
        Budget,
        Standard,
        Luxury,
        Boutique,
        Resort
    }

    public string Name { get; init; }
    public string Description { get; init; }
    public HotelTypeDto HotelType { get; init; }
    public AddressDto Address { get; init; }
    public ContactInfoDto ContactInfo { get; init; }
    public int StarRating { get; init; }
    public int HostId { get; init; }

    public record AddressDto(
        string City,
        decimal Latitude,
        decimal Longitude,
        string Country,
        string PostalCode,
        string State,
        string Street);

    public record ContactInfoDto(string Email, string Phone, string Website);
}