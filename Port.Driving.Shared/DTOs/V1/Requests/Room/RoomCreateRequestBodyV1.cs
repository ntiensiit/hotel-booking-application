namespace Port.Driving.Shared.DTOs.V1.Requests.Room;

public record RoomCreateRequestBodyV1
{
    public enum CurrencyDto
    {
        Usd,
        Eur,
        Gbp,
        Jpy
    }

    public enum RoomStatusDto
    {
        Available,
        Occupied,
        Maintenance,
        Reserved
    }

    public enum RoomTypeDto
    {
        Single,
        Double,
        Triple,
        Suite,
        Deluxe
    }

    public string RoomNumber { get; init; }
    public int Capacity { get; init; }
    public RoomTypeDto RoomType { get; init; }
    public RoomStatusDto Status { get; init; }
    public PricingDto Pricing { get; init; }
    public int HotelId { get; init; }

    public record PricingDto(MoneyDto BasePrice);

    public record MoneyDto(CurrencyDto Currency, decimal Amount);
}