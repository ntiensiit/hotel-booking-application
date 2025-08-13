namespace Port.Driving.Shared.DTOs.Shared;

public partial record FieldErrorResponse
{
    public string Field { get; set; } = Field;
    public string Error { get; set; } = Error;
}

public partial record FieldErrorResponse(string Field, string Error);