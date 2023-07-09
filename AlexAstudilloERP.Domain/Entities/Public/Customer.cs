namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Customer
{
    public long PersonId { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<CompanyCustomer> CompanyCustomers { get; set; } = new List<CompanyCustomer>();

    public virtual Person? Person { get; set; }
}
