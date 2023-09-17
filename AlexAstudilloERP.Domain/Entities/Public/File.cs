namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class File
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UserCode { get; set; } = null!;
}
