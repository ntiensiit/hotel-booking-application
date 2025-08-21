namespace Port.Driving.Shared.DTOs.Shared;

public record Error
{
    public string Message { get; set; }
    public string Description { get; set; }
}