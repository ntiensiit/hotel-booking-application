namespace Port.Driving.Shared.DTOs.Shared;

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string>? Errors { get; set; }

    public static ValidationResult Success()
    {
        return new ValidationResult { IsValid = true };
    }

    public static ValidationResult Fail(string error)
    {
        return new ValidationResult { IsValid = false, Errors = [error] };
    }

    public static ValidationResult Fail(IEnumerable<string> errors)
    {
        return new ValidationResult { IsValid = false, Errors = [.. errors] };
    }
}
