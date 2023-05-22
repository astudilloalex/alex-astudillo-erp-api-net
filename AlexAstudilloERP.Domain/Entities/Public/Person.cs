namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Person
{
    public long Id { get; set; }

    public short DocumentTypeId { get; set; }

    public short? GenderId { get; set; }

    public string IdCard { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SocialReason { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool JuridicalPerson { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Customer? Customer { get; set; }

    public virtual PersonDocumentType? DocumentType { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual User? User { get; set; }
}
