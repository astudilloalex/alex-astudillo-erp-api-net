using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Company
{
    public int Id { get; set; }

    public long PersonId { get; set; }

    public int? ParentId { get; set; }

    public string? Code { get; set; }

    public string Tradename { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public bool KeepAccount { get; set; }

    public bool SpecialTaxpayer { get; set; }

    public string? SpecialTaxpayerNumber { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

    [JsonIgnore]
    public virtual ICollection<Company> InverseParent { get; set; } = new List<Company>();

    public virtual Company? Parent { get; set; }

    public virtual Person? Person { get; set; }
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    [JsonIgnore]
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
