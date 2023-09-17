namespace AlexAstudilloERP.Domain.Entities.Json;

public partial class AuditDatum
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public short TableId { get; set; }

    public char Operation { get; set; }

    public string? OldData { get; set; }

    public string NewData { get; set; } = null!;

    public char Origin { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public string UserCode { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
