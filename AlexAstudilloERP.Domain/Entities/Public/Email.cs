using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class Email
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Mail { get; set; } = null!;

    public bool Verified { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }

    [JsonIgnore]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
