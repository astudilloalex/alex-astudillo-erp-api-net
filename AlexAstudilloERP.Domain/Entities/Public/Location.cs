using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Location
{
    public int Id { get; set; }

    public int PoliticalDivisionId { get; set; }

    public string Code { get; set; } = null!;

    public string MainStreet { get; set; } = null!;

    public string? SecondaryStreet { get; set; }

    public string? HouseNumber { get; set; }

    public string? PostalCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

    public virtual PoliticalDivision PoliticalDivision { get; set; } = null!;
}
