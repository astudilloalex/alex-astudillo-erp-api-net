﻿using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

/// <summary>
/// Implements a <see cref="INPRepository{T, ID}"/> and own methods or functions.
/// </summary>
public interface IRoleRepository : INPRepository<Role, int>
{
    /// <summary>
    /// Check if exists a role by id and company id.
    /// </summary>
    /// <param name="id">The role unique identifier.</param>
    /// <param name="companyId">The company unique identifier.</param>
    /// <returns>True if exists, otherwise null.</returns>
    public Task<bool> ExistsByIdAndCompanyId(int id, int companyId);
    /// <summary>
    /// Check if role exists by name and company id.
    /// </summary>
    /// <param name="companyId">The company unique identifier.</param>
    /// <param name="name">The name of the role.</param>
    /// <returns>True if exists, otherwise null.</returns>
    public Task<bool> ExistsByNameAndCompanyId(int companyId, string name);
    /// <summary>
    /// Find a role by name and company id.
    /// </summary>
    /// <param name="companyId">The company unique identifier.</param>
    /// <param name="name">The name of the role.</param>
    /// <returns>A <see cref="Role"/> if exists, otherwise null.</returns>
    public Task<Role?> FindByNameAndCompanyId(int companyId, string name);
    /// <summary>
    /// Check if the role is editable.
    /// </summary>
    /// <param name="roleId">The role unique identifier.</param>
    /// <param name="companyId">The company unique identifier.</param>
    /// <returns>True if is editable, otherwise null.</returns>
    public Task<bool> IsEditable(int id, int companyId);
}
