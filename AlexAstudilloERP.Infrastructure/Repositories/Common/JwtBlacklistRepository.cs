using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Common;

public class JwtBlacklistRepository(PostgreSQLContext _context) : NPPostgreSQLRepository<JwtBlacklist, int>(_context), IJwtBlacklistRepository
{
    public Task<int> DeleteExpired()
    {
        //return _context.JwtBlacklists.AsNoTracking().Where(x => x.ExpiresAt < DateTime.Now).ExecuteDeleteAsync();
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByJWTAsync(string token)
    {
        throw new NotImplementedException();
    }
}
