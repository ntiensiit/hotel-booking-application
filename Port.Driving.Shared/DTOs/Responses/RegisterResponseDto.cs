using Immutype;

namespace Port.Driving.Shared.DTOs.Responses;

[Target]
public record RegisterResponseDto(TokenResponseDto TokenResponseDto, UserInfoResponseDto UserInfoResponseDto);