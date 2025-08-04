using Shared.Errors;

namespace Shared.Results;

public record Result
{
    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    protected bool IsSuccess { get; }
    protected Error? Error { get; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    private static Result Failure(Error error)
    {
        return new Result(false, error ?? throw new ArgumentNullException(nameof(error)));
    }

    public static implicit operator Result(Error error)
    {
        return Failure(error);
    }
}

public record Result<T> : Result
{
    private Result(T value) : base(true, null)
    {
        Value = value;
    }

    private Result(Error error) : base(false, error)
    {
    }

    private T? Value { get; }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    public T GetValueOrThrow()
    {
        if (IsSuccess) return Value!;
        throw new InvalidOperationException($"Cannot retrieve value from a failed result. Error: {Error?.Message}");
    }
}