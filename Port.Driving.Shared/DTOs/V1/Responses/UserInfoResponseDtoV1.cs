namespace Port.Driving.Shared.DTOs.V1.Responses;

public partial record UserInfoResponseDtoV1
{
    public string FullName { get; set; } = FullName;
    public DateTime DateOfBirth { get; set; } =  DateOfBirth;
    public string Email { get; set; }  = Email;
    public string PhoneNumber { get; set; }   = PhoneNumber;
}

public partial record UserInfoResponseDtoV1(string FullName, DateTime DateOfBirth, string Email, string PhoneNumber);