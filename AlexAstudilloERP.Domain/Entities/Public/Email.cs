namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Email
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Mail { get; set; } = null!;

    public bool Verified { get; set; }

    public virtual User? User { get; set; }
}
