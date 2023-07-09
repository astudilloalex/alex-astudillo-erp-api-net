namespace AlexAstudilloERP.API.DTOs.Requests;

public class CustomerRequestDTO
{
    public int CompanyId { get; set; }
    public string? Email { get; set; }
    public AddressRequestDTO? Address { get; set; }
    public PersonRequestDTO Person { get; set; } = null!;
    public PhoneRequestDTO? Phone { get; set; }
}
