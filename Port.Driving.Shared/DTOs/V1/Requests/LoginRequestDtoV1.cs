namespace Port.Driving.Shared.DTOs.V1.Requests;

public partial record LoginRequestDtoV1
{
    public string Email { get; set; } = Email;
    public string Password { get; set; } = Password;
}

public partial record LoginRequestDtoV1(string Email, string Password);