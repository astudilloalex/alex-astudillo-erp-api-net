using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface ITokenService
{
    /// <summary>
    /// Generate a token for a specific user.
    /// </summary>
    /// <param name="user">The user to generate.</param>
    /// <returns>A JWT token.</returns>
    public string GenerateToken(User user);

    /// <summary>
    /// Generate a JWT Token for specific days.
    /// </summary>
    /// <param name="user">The user to generate token.</param>
    /// <param name="days">The number of days to valid the token.</param>
    /// <returns>A JWT Token.</returns>
    public string GenerateToken(User user, int days = 1);

    /// <summary>
    /// Get a user unique identifier based in the token.
    /// </summary>
    /// <param name="token">The token to get data.</param>
    /// <returns>A unique user identifier.</returns>
    public long GetUserId(string token);

    /// <summary>
    /// Return a username from token.
    /// </summary>
    /// <param name="token">The token to get data.</param>
    /// <returns>A username information.</returns>
    public string GetUsername(string token);
}
