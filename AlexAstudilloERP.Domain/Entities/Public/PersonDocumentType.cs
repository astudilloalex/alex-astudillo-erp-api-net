using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class PersonDocumentType
{
    public short Id { get; set; }

    public short CountryId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Country Country { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
