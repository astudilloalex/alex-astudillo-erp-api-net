using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual Company? Company { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    public virtual PersonDocumentType? DocumentType { get; set; }

    [JsonIgnore]
    public virtual Employee? Employee { get; set; }

    public virtual Gender? Gender { get; set; }

    [JsonIgnore]
    public virtual Supplier? Supplier { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
