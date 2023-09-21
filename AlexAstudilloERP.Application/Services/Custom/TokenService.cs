using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlexAstudilloERP.Application.Services.Custom;

/// <summary>
/// Implements <see cref="ITokenService"/>.
/// </summary>
public class TokenService : ITokenService
{
    /// <summary>
    /// The expiration time in seconds.
    /// </summary>
    private const int _expirationTime = 28800;

    /// <summary>
    /// The audience.
    /// </summary>
    private readonly string _audience;

    /// <summary>
    /// The issuer in this case "alexastudillo.com"
    /// </summary>
    private readonly string _issuer;

    /// <summary>
    /// The key to access data the minimum length is 64 characters.
    /// </summary>
    private readonly string _key;

    public TokenService(IConfiguration configuration)
    {
        _audience = configuration["JWT:Audience"]!;
        _issuer = configuration["JWT:Issuer"]!;
        _key = configuration["JWT:Key"]!;
    }

    public string GenerateToken(User user)
    {
        return new JwtSecurityTokenHandler().WriteToken(GetSecurityToken(user, _expirationTime));
    }

    public string GenerateToken(User user, int lifeTime = 3600)
    {
        return new JwtSecurityTokenHandler().WriteToken(GetSecurityToken(user, lifeTime));
    }

    public long GetUserId(string token)
    {
        _ = long.TryParse(GetClaims(token).FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Sid))?.Value, out long userId);
        return userId;
    }

    public string GetUsername(string token)
    {
        return GetClaims(token).FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Name))?.Value ?? "";
    }

    #region This is not part of the interface.
    private JwtSecurityToken GetSecurityToken(User user, int lifeTime)
    {
        // Add claims.
        List<Claim> claims = new()
        {
            new (ClaimTypes.Sid, user.PersonId.ToString()),
            new (ClaimTypes.Name, user.Username),
        };
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_key));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);
        return new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddSeconds(lifeTime),
            signingCredentials: credentials
        );
    }

    private List<Claim> GetClaims(string token)
    {
        JwtSecurityTokenHandler handler = new();
        ClaimsPrincipal claimsPrincipal = handler.ValidateToken(token, new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        }, out _);
        return claimsPrincipal.Claims.ToList();
    }
    #endregion
}
