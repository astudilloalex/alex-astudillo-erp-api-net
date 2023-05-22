namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class DialInCode
{
    public short Id { get; set; }

    public short CountryId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
