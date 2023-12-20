using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Domain.Interfaces.Services.Common;

namespace AlexAstudilloERP.Application.Services.Common;

public class JwtBlacklistService(IJwtBlacklistRepository _repository) : IJwtBlacklistService
{
    public Task<int> DeleteExpired()
    {
        return _repository.DeleteExpired();
    }

    public Task<bool> ExistsByToken(string token)
    {
        return _repository.ExistsByJWTAsync(token);
    }

    public async Task<JwtBlacklist> Save(JwtBlacklist jwtBlacklist)
    {
        return await _repository.SaveAsync(jwtBlacklist);
    }
}
