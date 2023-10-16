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

    public Task<List<Menu>> FindByParentIdAsync(short parentId)
    {
        string query = @"WITH RECURSIVE menu_hierarchy AS (
	SELECT * FROM menus WHERE parent_id = {0}
	UNION ALL
	SELECT m.* FROM menus m INNER JOIN menu_hierarchy mh
	ON m.parent_id = mh.id
) SELECT * FROM menu_hierarchy";
        return _context.Menus.FromSqlRaw(query, parentId).AsNoTracking()
            .ToListAsync();

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
) ORDER BY m.order ASC";
        return _context.Menus.FromSqlRaw(query, new object[] { userCode, companyCode }).AsNoTracking().ToListAsync();
    }

    public Task<List<Menu>> FindParentsAsync(string userCode, string companyCode, bool? isPublic = null)
    {
        string query;
        if (isPublic == null)
        {
            query = @"SELECT m.*
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
) AND m.parent_id IS NULL ORDER BY m.order ASC";
            return _context.Menus.FromSqlRaw(query, new object[] { userCode, companyCode }).AsNoTracking().ToListAsync();
        }
        else if (!isPublic.Value)
        {
            query = @"SELECT m.*
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
) AND m.parent_id IS NULL AND m.is_public = {2} ORDER BY m.order ASC";
            return _context.Menus.FromSqlRaw(query, new object[] { userCode, companyCode, isPublic }).AsNoTracking().ToListAsync();
        }
        return _context.Menus.AsNoTracking()
            .Where(m => m.IsPublic == isPublic && m.ParentId == null)
            .ToListAsync();
    }
}
