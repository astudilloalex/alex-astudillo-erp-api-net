using System.Text.Json.Serialization;

namespace AlexAstudilloERP.Domain.Models.FirebaseAuth;

public class FirebaseSignInResponse
{
    [JsonPropertyName("idToken")]
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string ExpiresIn { get; set; } = "3600";
    public string LocalId { get; set; } = null!;
}
