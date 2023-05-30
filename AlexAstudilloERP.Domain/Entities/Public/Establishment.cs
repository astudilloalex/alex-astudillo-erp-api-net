using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Establishment
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int AddressId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Main { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Company? Company { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
