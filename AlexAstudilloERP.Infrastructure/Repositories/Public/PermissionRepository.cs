using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PermissionRepository : NPPostgreSQLRepository<Permission, short>, IPermissionRepository
{
    private readonly PostgreSQLContext _context;

    public PermissionRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> HasEstablishmentPermission(long userId, int establishmentId, PermissionEnum permission)
    {
        short permissionId = (short)permission;
        string query = "SELECT p.* FROM permissions p " +
            "INNER JOIN role_permissions rp ON rp.permission_id = p.id " +
            "INNER JOIN roles r ON r.id = rp.role_id " +
            "INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "INNER JOIN users u ON u.person_id = ur.user_id " +
            "INNER JOIN user_establishments ue ON ue.user_id = u.person_id " +
            "INNER JOIN establishments e ON e.id = ue.establishment_id AND e.company_id = r.company_id " +
            "WHERE u.person_id = {0} AND e.id = {1} AND p.id = {2}";
        return _context.Permissions.FromSqlRaw(query, new object[] { userId, establishmentId, permissionId }).AsNoTracking()
            .AnyAsync();
    }

    public Task<bool> HasPermission(long userId, int companyId, PermissionEnum permission)
    {
        short permissionId = (short)permission;
        string query = "SELECT p.* FROM permissions p " +
            "INNER JOIN role_permissions rp ON rp.permission_id = p.id " +
            "INNER JOIN roles r ON r.id = rp.role_id " +
            "INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "WHERE ur.user_id = {0} AND r.company_id = {1} AND p.id = {2}";
        return _context.Permissions.FromSqlRaw(query, new object[] { userId, companyId, permissionId }).AsNoTracking()
            .AnyAsync();
    }
}
