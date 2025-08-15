namespace Port.Driving.Shared.DTOs.V1.Requests.User;

public record UserCreateRequestBodyV1
{
    public string FullName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string PasswordConfirmed { get; init; }
}