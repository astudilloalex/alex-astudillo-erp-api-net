namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Customer
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SocialReason { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
