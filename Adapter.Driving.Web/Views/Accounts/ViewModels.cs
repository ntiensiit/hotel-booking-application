namespace Adapter.Driving.Web.Views.Accounts;

public record UserRegisterViewModel
{
    public string CustomerName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string PasswordConfirmed { get; init; }
}

public record UserLoginViewModel
{
    public string UserName { get; init; }
    public string Password { get; init; }
}