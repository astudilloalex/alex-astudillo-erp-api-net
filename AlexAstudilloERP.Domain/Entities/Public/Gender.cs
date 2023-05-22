namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Gender
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
