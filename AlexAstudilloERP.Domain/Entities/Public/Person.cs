using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Person
{
    public int Id { get; set; }

    public short PersonDocumentTypeId { get; set; }

    public short? GenderId { get; set; }

    public string Code { get; set; } = null!;

    public string IdCard { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SocialReason { get; set; }

    public DateTime? Birthdate { get; set; }

    public bool JuridicalPerson { get; set; }

    public bool IdCardVerified { get; set; }

    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Gender? Gender { get; set; }

    public virtual PersonDocumentType PersonDocumentType { get; set; } = null!;

    public virtual User? User { get; set; }
}
