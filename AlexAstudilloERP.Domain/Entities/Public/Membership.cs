namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Membership
{
    public short Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
