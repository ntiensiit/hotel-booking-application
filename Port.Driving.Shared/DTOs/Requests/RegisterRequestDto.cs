using Immutype;

namespace Port.Driving.Shared.DTOs.Requests;

[Target]
public record RegisterRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}