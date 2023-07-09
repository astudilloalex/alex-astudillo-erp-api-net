using AlexAstudilloERP.Domain.Entities.Json;
using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class User
{
    public long PersonId { get; set; }

    public int EmailId { get; set; }

    public string? Code { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool AccountNonExpired { get; set; }

    public bool AccountNonLocked { get; set; }

    public bool CredentialsNonExpired { get; set; }

    public bool Enabled { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<AuditDatum> AuditData { get; set; } = new List<AuditDatum>();

    [JsonIgnore]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Email? Email { get; set; }

    [JsonIgnore]
    public virtual ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

    public virtual Person? Person { get; set; }

    [JsonIgnore]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Establishment> EstablishmentsNavigation { get; set; } = new List<Establishment>();

    public virtual ICollection<Role> RolesNavigation { get; set; } = new List<Role>();
}
