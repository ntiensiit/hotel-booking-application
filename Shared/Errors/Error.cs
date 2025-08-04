namespace Shared.Errors;

public enum ErrorType
{
    NotFound,
    Validation,
    Unauthorized,
    ServerError
}

public record Error(string Id, ErrorType Type, string Message);

public static class Errors
{
    public static Error InsufficientFunds { get; } =
        new("InsufficientFunds", ErrorType.Validation, "Insufficient balance.");

    public static Error AccountNotFound { get; } =
        new("AccountNotFound", ErrorType.NotFound, "Account not found.");

    public static Error AccountAlreadyExists { get; } =
        new("AccountAlreadyExists", ErrorType.Validation, "Account already exists.");

    public static Error AccountNotActive { get; } =
        new("AccountNotActive", ErrorType.Validation, "Account is not active.");

    public static Error AccountUnauthorized { get; } =
        new("AccountUnauthorized", ErrorType.Unauthorized, "Account unauthorized.");

    public static Error ServerError { get; } =
        new("ServerError", ErrorType.ServerError, "Server error.");
}