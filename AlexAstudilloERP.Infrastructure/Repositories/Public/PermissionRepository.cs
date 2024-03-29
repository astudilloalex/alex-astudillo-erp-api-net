﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PermissionRepository(PostgreSQLContext context) : NPPostgreSQLRepository<Permission, short>(context), IPermissionRepository
{
    private readonly PostgreSQLContext _context = context;

    public async Task<IPage<Permission>> FindAllAsync(IPageable pageable, int companyId, long userId)
    {
        string query = "SELECT DISTINCT p.* FROM permissions p " +
            "INNER JOIN role_permissions rp ON rp.permission_id = p.id " +
            "INNER JOIN roles r ON r.id = rp.role_id " +
            "INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "WHERE r.company_id = {0} AND ur.user_id = {1} AND p.active";
        int offset = Convert.ToInt32(pageable.GetOffset());
        List<Permission> data = await _context.Permissions.FromSqlRaw(query, [companyId, userId]).AsNoTracking()
            .OrderBy(p => p.Code)
            .Skip(offset)
            .Take(pageable.GetPageSize())
            .ToListAsync();
        return new Page<Permission>(data, pageable, await _context.Permissions.FromSqlRaw(query, [companyId, userId]).AsNoTracking().LongCountAsync());
    }

    public Task<List<Permission>> FindByCompanyIdAndUserId(int companyId, long userId, List<short> permissionIds)
    {
        string query = "SELECT p.* FROM permissions p " +
            "INNER JOIN role_permissions rp ON rp.permission_id = p.id " +
            "INNER JOIN roles r ON r.id = rp.role_id " +
            "INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "WHERE ur.user_id = {0} AND r.company_id = {1}";
        return _context.Permissions.FromSqlRaw(query, [userId, companyId]).AsNoTracking()
            .Where(p => permissionIds.Contains(p.Id))
            .ToListAsync();
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
        return _context.Permissions.FromSqlRaw(query, [userId, establishmentId, permissionId]).AsNoTracking()
            .AnyAsync();
    }

    public Task<bool> HasPermission(string userCode, string companyCode, PermissionEnum permission)
    {
        string query = "SELECT p.* FROM permissions p " +
            "INNER JOIN role_permissions rp ON rp.permission_id = p.id " +
            "INNER JOIN roles r ON r.id = rp.role_id " +
            "INNER JOIN companies c ON c.id = r.company_id " +
            "INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "INNER JOIN users u ON u.id = ur.user_id " +
            "WHERE c.active AND r.active AND p.active AND u.code = {0} AND c.code = {1} AND p.id = {2}";
        return _context.Permissions.FromSqlRaw(query, [userCode, companyCode, (short)permission]).AsNoTracking()
            .AnyAsync();
    }
}
