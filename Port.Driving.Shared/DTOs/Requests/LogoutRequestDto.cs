using System.ComponentModel.DataAnnotations;
using Immutype;

namespace Port.Driving.Shared.DTOs.Requests;

[Target]
public record LogoutRequestDto
{
    [Required] public string RefreshToken { get; set; }
}