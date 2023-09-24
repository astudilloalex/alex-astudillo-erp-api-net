namespace AlexAstudilloERP.API.DTOs;

public class CompanyDTO
{
    public PersonDTO Person { get; set; } = null!;

    public string? Code { get; set; }

    public string Tradename { get; set; } = null!;

    public string? Description { get; set; }
}
