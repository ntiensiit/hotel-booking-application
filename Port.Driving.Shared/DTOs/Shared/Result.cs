namespace Port.Driving.Shared.DTOs.Shared;

public record Result
{
    private readonly Success? _success;

    private Result(Success? success, IEnumerable<Error>? errors)
    {
        _success = success;
        Errors = errors;
    }

    public Result(IEnumerable<Error> errors)
        : this(null, errors) { }

    public Result(Success success)
        : this(success, []) { }

    public string SuccessMessage => _success?.ToString() ?? string.Empty;
    public IEnumerable<Error>? Errors { get; }

    public bool IsSuccess => Errors == null || !Errors.Any();
}

public record Result<T> : Result
{
    private readonly T? _value;

    public Result(T value, Success success)
        : base(success)
    {
        _value = value;
    }
}
