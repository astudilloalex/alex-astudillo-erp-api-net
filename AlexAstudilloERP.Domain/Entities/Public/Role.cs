using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Role
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool Editable { get; } = true;

    public virtual Company? Company { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
