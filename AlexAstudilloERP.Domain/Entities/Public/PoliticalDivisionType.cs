namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class PoliticalDivisionType
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<PoliticalDivision> PoliticalDivisions { get; set; } = new List<PoliticalDivision>();
}
