﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

/// <summary>
/// Implements <see cref="IRoleRepository"/>.
/// </summary>
public class RoleRepository : NPPostgreSQLRepository<Role, int>, IRoleRepository
{
    private readonly PostgreSQLContext _context;

    public RoleRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsByIdAndCompanyId(int id, int companyId)
    {
        return _context.Roles.AsNoTracking().AnyAsync(r => r.Id == id && r.CompanyId == companyId);
    }

    public Task<bool> ExistsByNameAndCompanyId(int companyId, string name)
    {
        return _context.Roles.AsNoTracking().AnyAsync(r => r.Name.Equals(name) && r.CompanyId == companyId);
    }

    public Task<Role?> FindByNameAndCompanyId(int companyId, string name)
    {
        return _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name.Equals(name) && r.CompanyId == companyId);
    }

    public Task<bool> IsEditable(int id, int companyId)
    {
        return _context.Roles.AsNoTracking()
            .Where(r => r.Id == id && r.CompanyId == companyId)
            .Select(r => r.Editable)
            .FirstOrDefaultAsync();
    }

    public new async ValueTask<Role> SaveAsync(Role entity)
    {
        foreach (Permission permission in entity.Permissions) _context.Permissions.Attach(permission);
        entity.Permissions = _context.Permissions.Local.Where(p => entity.Permissions.Select(per => per.Id).Contains(p.Id)).ToList();
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public new async ValueTask<Role> UpdateAsync(Role entity)
    {
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                Role finded = await _context.Roles.SingleAsync(r => r.Id == entity.Id);
                await _context.Database.ExecuteSqlAsync($"DELETE FROM role_permissions WHERE role_id = {entity.Id}");
                foreach (Permission permission in entity.Permissions) _context.Permissions.Attach(permission);
                finded.Permissions = _context.Permissions.Local.Where(p => entity.Permissions.Select(per => per.Id).Contains(p.Id)).ToList();
                finded.Active = entity.Active;
                finded.Name = entity.Name;
                finded.Description = entity.Description;
                await _context.SaveChangesAsync();
                entity.Code = finded.Code;
                entity.CreationDate = finded.CreationDate;
                entity.UpdateDate = finded.UpdateDate;
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return entity;
    }
}