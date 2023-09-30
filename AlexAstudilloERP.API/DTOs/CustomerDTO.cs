namespace AlexAstudilloERP.API.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Code { get; set; } = "";

    public short PersonDocumentTypeId { get; set; }

    public string IdCard { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SocialReason { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool JuridicalPerson { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }
}
