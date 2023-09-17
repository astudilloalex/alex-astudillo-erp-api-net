using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Common;

/// <summary>
/// Save all datatypes for fields
/// </summary>
public partial class DataType
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<EquivalenceTable> EquivalenceTableDataTypes { get; set; } = new List<EquivalenceTable>();

    [JsonIgnore]
    public virtual ICollection<EquivalenceTable> EquivalenceTableLocalDataTypes { get; set; } = new List<EquivalenceTable>();
}
