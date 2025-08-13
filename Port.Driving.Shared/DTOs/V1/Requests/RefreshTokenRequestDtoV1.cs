using System.ComponentModel.DataAnnotations;

namespace Port.Driving.Shared.DTOs.V1.Requests;

public partial record RefreshTokenRequestDtoV1
{
    [Required] public string RefreshToken { get; set; } = RefreshToken;
}

public partial record RefreshTokenRequestDtoV1(string RefreshToken);