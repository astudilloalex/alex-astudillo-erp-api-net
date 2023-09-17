using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Establishment
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int LocationId { get; set; }

    public short EstablishmentTypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual EstablishmentType EstablishmentType { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;
}
