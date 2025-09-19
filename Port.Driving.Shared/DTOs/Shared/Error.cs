namespace Port.Driving.Shared.DTOs.Shared;

public partial record Error
{
    public string? Message { get; set; } = Message;
    public string? Description { get; set; } = Description;
}

public partial record Error(string? Message, string? Description);
