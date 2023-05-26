using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Address
{
    public int Id { get; set; }

    public int PoliticalDivisionId { get; set; }

    public string? Code { get; set; }

    public string MainStreet { get; set; } = null!;

    public string? SecondaryStreet { get; set; }

    public string? HouseNumber { get; set; }

    public string? PostalCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [JsonIgnore]
    public virtual ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

    public virtual PoliticalDivision? PoliticalDivision { get; set; }

    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
