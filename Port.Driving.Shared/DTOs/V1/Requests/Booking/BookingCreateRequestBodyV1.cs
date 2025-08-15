namespace Port.Driving.Shared.DTOs.V1.Requests.Booking;

public record BookingCreateRequestBodyV1
{
    public DateOnly CheckInDate { get; init; }
    public DateOnly CheckOutDate { get; init; }
    public int NumberOfGuests { get; init; }
    public int HotelId { get; init; }
    public int RoomId { get; init; }
}