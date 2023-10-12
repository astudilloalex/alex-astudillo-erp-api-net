using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class MenuRepository : IMenuRepository
{
    private readonly PostgreSQLContext _context;

    public MenuRepository(PostgreSQLContext context)
    {
        _context = context;
    }

    public Task<List<Menu>> FindByUserCodeAndCompanyCodeAsync(string userCode, string companyCode)
    {
        string query = @"SELECT m.*
FROM menus m
WHERE EXISTS (
    SELECT 1
    FROM permission_menus pm
    INNER JOIN permissions p ON pm.permission_id = p.id
    INNER JOIN role_permissions rp ON p.id = rp.permission_id
    INNER JOIN roles r ON rp.role_id = r.id
    INNER JOIN user_roles ur ON r.id = ur.role_id
    INNER JOIN users u ON ur.user_id = u.id
    INNER JOIN companies c ON r.company_id = c.id
    WHERE u.code = {0} AND c.code = {1}
    AND m.id = pm.menu_id
)";
        return _context.Menus.FromSqlRaw(query, new object[] { userCode, companyCode }).AsNoTracking().ToListAsync();
    }
}
