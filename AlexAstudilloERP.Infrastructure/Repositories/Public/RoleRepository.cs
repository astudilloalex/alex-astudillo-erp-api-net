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

    public Task<bool> ExistsByNameAndCompanyId(int companyId, string name)
    {
        return _context.Roles.AsNoTracking().AnyAsync(r => r.Name.Equals(name) && r.CompanyId == companyId);
    }

    public Task<Role?> FindByNameAndCompanyId(int companyId, string name)
    {
        return _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name.Equals(name) && r.CompanyId == companyId);
    }

    public new async ValueTask<Role> SaveAsync(Role entity)
    {
        foreach (Permission permission in entity.Permissions) _context.Permissions.Attach(permission);
        entity.Permissions = _context.Permissions.Local.Where(p => entity.Permissions.Select(per => per.Id).Contains(p.Id)).ToList();
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
