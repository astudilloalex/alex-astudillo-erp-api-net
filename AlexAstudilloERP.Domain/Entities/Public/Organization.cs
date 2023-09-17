using AlexAstudilloERP.Domain.Entities.Integration;
using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Organization
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [JsonIgnore]
    public virtual ICollection<EquivalenceTable> EquivalenceTables { get; set; } = new List<EquivalenceTable>();

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
