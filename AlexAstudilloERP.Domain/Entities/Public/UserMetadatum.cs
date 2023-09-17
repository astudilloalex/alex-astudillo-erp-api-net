using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Entities.Public;

public partial class UserMetadatum
{
    public int UserId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? LastRefreshDate { get; set; }

    public DateTime? LastSignInDate { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
