namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class UserMembership
{
    public int UserId { get; set; }

    public short MembershipId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Membership Membership { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
