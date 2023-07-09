namespace AlexAstudilloERP.API.DTOs.Requests;

public class PersonRequestDTO
{
    public short DocumentTypeId { get; set; }
    public short? GenderId { get; set; }
    public string IdCard { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SocialReason { get; set; }
    public DateTime? Birthdate { get; set; }
    public bool JuridicalPerson { get; set; }
}
