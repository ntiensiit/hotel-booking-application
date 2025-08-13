using System.Text.Json.Serialization;

namespace Port.Driving.Shared.DTOs.Shared;

public partial record ValidationErrorResponse
{
    public int StatusCode { get; set; } = StatusCode;
    public string Message { get; set; } = Message;
    [JsonPropertyName("Errors")] public List<FieldErrorResponse> FieldErrors { get; set; } = new();
}

public partial record ValidationErrorResponse(int StatusCode, string Message);

public partial record ValidationErrorResponse
{
    public void Reset()
    {
        StatusCode = 0;
        Message = string.Empty;
        FieldErrors = new List<FieldErrorResponse>();
    }
}