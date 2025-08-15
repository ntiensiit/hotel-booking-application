namespace Port.Driving.Shared.DTOs.Shared;

public partial record FieldErrorResponse
{
    public string Field { get; set; } = Field;
    public string Message { get; set; } = Message;
}

public partial record FieldErrorResponse(string Field, string Message);