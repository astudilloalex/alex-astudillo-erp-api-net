using AlexAstudilloERP.Domain.Entities.Integration;
using AlexAstudilloERP.Domain.Entities.Json;
using AlexAstudilloERP.Domain.Entities.Public;
using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Common;

/// <summary>
/// Save all available tables on the database
/// </summary>
public partial class Table
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<AuditDatum> AuditData { get; set; } = new List<AuditDatum>();

    [JsonIgnore]
    public virtual ICollection<EquivalenceTable> EquivalenceTables { get; set; } = new List<EquivalenceTable>();

    [JsonIgnore]
    public virtual ICollection<UserRelationship> UserRelationships { get; set; } = new List<UserRelationship>();
}
