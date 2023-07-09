namespace AlexAstudilloERP.API.DTOs.Requests;

public class AddressRequestDTO
{
    public int PoliticalDivisionId { get; set; }

    public string MainStreet { get; set; } = null!;

    public string? SecondaryStreet { get; set; }

    public string? HouseNumber { get; set; }

    public string? PostalCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}
