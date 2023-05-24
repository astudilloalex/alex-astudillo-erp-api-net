namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Phone
{
    public int Id { get; set; }

    public short DialInCodeId { get; set; }

    public string? Code { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool Verified { get; set; }

    public virtual DialInCode? DialInCode { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
