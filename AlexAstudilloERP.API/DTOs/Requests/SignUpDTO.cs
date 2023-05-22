namespace AlexAstudilloERP.API.DTOs.Requests;

public class SignUpDTO
{
    public short DocumentTypeId { get; set; }
    public bool JuridicalPerson { get; set; }
    public string IdCard { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SocialReason { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
