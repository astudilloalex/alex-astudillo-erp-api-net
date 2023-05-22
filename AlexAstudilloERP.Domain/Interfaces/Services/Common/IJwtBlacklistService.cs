using AlexAstudilloERP.Domain.Entities.Common;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Common;

public interface IJwtBlacklistService
{
    public Task<bool> ExistsByToken(string token);

    public Task<JwtBlacklist> Save(JwtBlacklist jwtBlacklist);

    public Task<int> DeleteExpired();
}
