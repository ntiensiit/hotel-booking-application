using System.Text.Json.Serialization;

namespace Port.Driving.Shared.DTOs.Shared;

public partial record ValidationErrorResponse
{
    public int StatusCode { get; set; } = StatusCode;
    public string Description { get; set; } = Description;
    [JsonPropertyName("Details")] public List<FieldErrorResponse> FieldErrors { get; set; } = new();
}

public partial record ValidationErrorResponse(int StatusCode, string Description);

public partial record ValidationErrorResponse
{
    public void Reset()
    {
        StatusCode = 0;
        Description = string.Empty;
        FieldErrors = new List<FieldErrorResponse>();
    }
}