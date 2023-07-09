using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Entities.Json;

public partial class AuditDatum
{
    public long Id { get; set; }

    public short TableId { get; set; }

    public char Operation { get; set; }

    public string? OldData { get; set; }

    public string NewData { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public long UserId { get; set; }

    public virtual DatabaseTable? Table { get; set; }

    public virtual User? User { get; set; }
}
