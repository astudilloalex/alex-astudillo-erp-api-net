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

    public virtual Email? Email { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
