namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class PoliticalDivision
{
    public int Id { get; set; }

    public short CountryId { get; set; }

    public short PoliticalDivisionTypeId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Country? Country { get; set; }

    public virtual PoliticalDivisionType? PoliticalDivisionType { get; set; } = null!;
}
