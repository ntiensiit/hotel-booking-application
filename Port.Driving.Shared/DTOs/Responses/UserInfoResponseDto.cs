using Immutype;

namespace Port.Driving.Shared.DTOs.Responses;

[Target]
public record UserInfoResponseDto(string FullName, int Age, string Email, string PhoneNumber);