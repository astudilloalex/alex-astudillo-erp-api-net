using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class RoleRepository : NPPostgreSQLRepository<Role, int>, IRoleRepository
{
    private readonly PostgreSQLContext _context;

    public RoleRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsName(int companyId, string name)
    {
        return _context.Roles.AsNoTracking().AnyAsync(r => r.Name.Equals(name) && r.CompanyId == companyId);
    }
}
