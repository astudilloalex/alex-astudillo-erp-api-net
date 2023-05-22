using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Domain.Interfaces.Services.Common;

namespace AlexAstudilloERP.Application.Services.Common;

public class JwtBlacklistService : IJwtBlacklistService
{
    private readonly IJwtBlacklistRepository _repository;

    public JwtBlacklistService(IJwtBlacklistRepository repository)
    {
        _repository = repository;
    }

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
