using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Country
{
    public short Id { get; set; }

    /// <summary>
    /// ISO 3166-1 alpha-2 codes are two-letter country codes defined in ISO 3166-1, part of the ISO 3166 standard[1] published by the International Organization for Standardization (ISO), to represent countries, dependent territories, and special areas of geographical interest.
    /// </summary>
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<DialInCode> DialInCodes { get; set; } = new List<DialInCode>();

    [JsonIgnore]
    public virtual ICollection<PoliticalDivision> PoliticalDivisions { get; set; } = new List<PoliticalDivision>();
}
