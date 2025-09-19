namespace Port.Driving.Shared.DTOs.Shared;

public record Success
{
    private readonly string _message;

    private Success(string msg)
    {
        _message = msg;
    }

    public static implicit operator string(Success success)
    {
        return success._message;
    }

    public static implicit operator Success(string msg)
    {
        return new Success(msg);
    }

    public override string ToString()
    {
        return _message;
    }
}
