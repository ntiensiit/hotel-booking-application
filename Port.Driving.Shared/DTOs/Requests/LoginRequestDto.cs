using Immutype;

namespace Port.Driving.Shared.DTOs.Requests;

[Target]
public record LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}