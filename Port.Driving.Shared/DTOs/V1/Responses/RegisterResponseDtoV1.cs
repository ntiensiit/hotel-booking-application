using System.Text.Json.Serialization;

namespace Port.Driving.Shared.DTOs.V1.Responses;

public partial record RegisterResponseDtoV1
{
    [JsonPropertyName("user")] public UserInfoResponseDtoV1 UserInfoResponseDtoV1 { get; } = UserInfoResponseDtoV1;
}

public partial record RegisterResponseDtoV1(UserInfoResponseDtoV1 UserInfoResponseDtoV1);