using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.API.Mappers;

public static class DTOToEntity
{
    public static Role RoleRequestDTOToRole(RoleRequestDTO dto)
    {
        List<Permission> permissions = new();
        foreach (short permissionId in dto.PermissionIds) permissions.Add(new() { Id = permissionId });
        return new()
        {
            CompanyId = dto.CompanyId,
            Name = dto.Name,
            Description = dto.Description,
            Permissions = permissions,
            Active = dto.Active,
        };
    }
}
