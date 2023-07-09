namespace AlexAstudilloERP.API.DTOs.Requests;

public class PhoneRequestDTO
{
    public short DialInCodeId { get; set; }
    public string PhoneNumber { get; set; } = null!;
}
