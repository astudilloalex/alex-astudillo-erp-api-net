namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Supplier
{
    public long PersonId { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? Code { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
