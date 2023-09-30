using AlexAstudilloERP.Domain.Entities.Common;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class UserRelationship
{
    public int ObjectId { get; set; }

    public short TableId { get; set; }

    public int UserId { get; set; }

    public virtual Table Table { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
