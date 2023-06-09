﻿using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Common;

public class JwtBlacklistRepository : NPPostgreSQLRepository<JwtBlacklist, int>, IJwtBlacklistRepository
{
    private readonly PostgreSQLContext _context;

    public JwtBlacklistRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<int> DeleteExpired()
    {
        return _context.JwtBlacklists.AsNoTracking().Where(x => x.ExpiresAt < DateTime.Now).ExecuteDeleteAsync();
    }

    public Task<bool> ExistsByJWTAsync(string token)
    {
        return _context.JwtBlacklists.AsNoTracking().AnyAsync(x => x.Token.Equals(token));
    }
}
