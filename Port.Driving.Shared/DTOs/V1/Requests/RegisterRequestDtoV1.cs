namespace Port.Driving.Shared.DTOs.V1.Requests;

public partial record RegisterRequestDtoV1
{
    public string UserName { get; set; } = UserName;
    public string Password { get; set; } = Password;
    public string ConfirmPassword { get; set; } = ConfirmPassword;
    public string FullName { get; set; } = FullName;
    public DateTime DateOfBirth { get; set; } = DateOfBirth;
    public string Email { get; set; } = Email;
    public string PhoneNumber { get; set; } = PhoneNumber;
}

public partial record RegisterRequestDtoV1(
    string UserName,
    string Password,
    string ConfirmPassword,
    string FullName,
    DateTime DateOfBirth,
    string Email,
    string PhoneNumber);