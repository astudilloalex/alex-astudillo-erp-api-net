namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class CompanyCustomer
{
    public int CompanyId { get; set; }

    public long CustomerId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool Active { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Customer? Customer { get; set; }
}
