namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class User
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string Code { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? DisplayName { get; set; }

    public string? PhotoUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public string? TenantId { get; set; }

    public string? CustomClaims { get; set; }

    public bool EmailVerified { get; set; }

    public bool Disabled { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();

    public virtual UserMetadatum? UserMetadatum { get; set; }

    public virtual ICollection<UserRelationship> UserRelationships { get; set; } = new List<UserRelationship>();

    public virtual ICollection<AuthProvider> AuthProviders { get; set; } = new List<AuthProvider>();

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
