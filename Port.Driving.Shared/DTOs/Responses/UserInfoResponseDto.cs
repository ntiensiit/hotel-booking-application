using Immutype;

namespace Port.Driving.Shared.DTOs.Responses;

[Target]
public record UserInfoResponseDto(string FullName, DateTime DateOfBirth, string Email, string PhoneNumber);