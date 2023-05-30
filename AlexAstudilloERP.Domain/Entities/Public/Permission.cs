using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Permission
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    [JsonIgnore]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
