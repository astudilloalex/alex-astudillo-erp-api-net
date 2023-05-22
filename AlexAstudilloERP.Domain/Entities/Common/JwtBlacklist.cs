namespace AlexAstudilloERP.Domain.Entities.Common;

public partial class JwtBlacklist
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}
