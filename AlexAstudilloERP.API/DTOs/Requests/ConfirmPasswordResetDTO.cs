namespace AlexAstudilloERP.API.DTOs.Requests;

public class ConfirmPasswordResetDTO
{
    public string OobCode { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
