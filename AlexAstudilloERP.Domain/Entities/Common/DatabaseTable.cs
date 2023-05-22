namespace AlexAstudilloERP.Domain.Entities.Common;

public partial class DatabaseTable
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
