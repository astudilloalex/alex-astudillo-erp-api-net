using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Integration;

public partial class Microservice
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<EquivalenceTable> EquivalenceTables { get; set; } = new List<EquivalenceTable>();
}
