using Immutype;

namespace Port.Driving.Shared.DTOs.Responses;

[Target]
public record TokenResponseDto(string AccessToken, string? RefreshToken);