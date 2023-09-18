namespace AlexAstudilloERP.API.DTOs.Requests;

public class SignInDTORequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
