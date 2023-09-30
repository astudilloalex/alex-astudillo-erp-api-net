using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;
using System.ComponentModel.Design;
using System.Data;

namespace AlexAstudilloERP.Application.Services.Public;

public class RoleService : IRoleService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _repository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public RoleService(IPermissionRepository permissionRepository,
        IRoleRepository repository, IValidateData validateData, ISetData setData)
    {
        _permissionRepository = permissionRepository;
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
    }

    public async Task<Role> Add(Role role, string token)
    {
        // Verify permissions.
        bool hasPermission = await _permissionRepository.HasPermission(1, role.CompanyId, PermissionEnum.RoleCreate);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        // Set data and validate common data.
        _setData.SetRoleData(role: role, update: false);
        role.Permissions = await _permissionRepository.FindByCompanyIdAndUserId(
            companyId: role.CompanyId,
            userId: 1,
            permissionIds: role.Permissions.Select(p => p.Id).ToList()
        );
        //await _validateData.ValidateRole(role: role, update: false);
        //role.UserId = userId;
        return await _repository.SaveAsync(role);
    }

    public async Task<IPage<Role>> GetAll(IPageable pageable, int companyId, string token, bool? active = null)
    {
        bool hasPermission = await _permissionRepository.HasPermission(1, companyId, PermissionEnum.RoleList);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByCompanyId(pageable, companyId, active);
    }

    public async Task<Role?> GetByCode(string code, string token)
    {
        Role? finded = await _repository.FindByCode(code);
        if (finded == null) return null;
        bool hasPermission = await _permissionRepository.HasPermission(1, finded.CompanyId, PermissionEnum.RoleGet);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return finded;
    }

    public async Task<Role> Update(Role role, string token)
    {
        bool hasPermission = await _permissionRepository.HasPermission(1, role.CompanyId, PermissionEnum.RoleUpdate);
        bool roleExist = await _repository.ExistsByIdAndCompanyId(role.Id, role.CompanyId);
        bool isEditable = await _repository.IsEditable(role.Id, role.CompanyId);
        if (!hasPermission || !roleExist || !isEditable) throw new ForbiddenException(ExceptionEnum.Forbidden);
        // Set data and validate common data.
        _setData.SetRoleData(role: role, update: true);
        role.Permissions = await _permissionRepository.FindByCompanyIdAndUserId(
            companyId: role.CompanyId,
            userId: 1,
            permissionIds: role.Permissions.Select(p => p.Id).ToList()
        );
        //await _validateData.ValidateRole(role: role, update: true);
        //role.UserId = userId;
        return await _repository.UpdateAsync(role);
    }
}
