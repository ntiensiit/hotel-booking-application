namespace Adapter.Driving.ResourceServer.DTOs.V1.Requests.Review;

public record ReviewCreateRequestBodyV1
{
    public string Comment { get; init; }
    public RatingDto Rating { get; init; }
    public int HotelId { get; init; }
    public int BookingId { get; init; }

    public record RatingDto(
        int CleanlinessRating,
        int LocationRating,
        int OverallRating,
        int ServiceRating,
        int ValueRating
    );
}
