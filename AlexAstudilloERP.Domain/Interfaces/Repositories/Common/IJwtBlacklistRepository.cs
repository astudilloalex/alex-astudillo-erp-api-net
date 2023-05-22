using AlexAstudilloERP.Domain.Entities.Common;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Common;

public interface IJwtBlacklistRepository : INPRepository<JwtBlacklist, int>
{

    /// <summary>
    /// Delete all expired tokens.
    /// </summary>
    /// <returns>A affected rows.</returns>
    public Task<int> DeleteExpired();

    /// <summary>
    /// Verify if the JWT exists.
    /// </summary>
    /// <param name="token">The token to verify if exists.</param>
    /// <returns>A true if exists, otherwise false.</returns>
    public Task<bool> ExistsByJWTAsync(string token);
}
