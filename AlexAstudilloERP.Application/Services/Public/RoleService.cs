using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class RoleService : IRoleService
{
    private readonly ITokenService _tokenService;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _repository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public RoleService(ITokenService tokenService, IPermissionRepository permissionRepository,
        IRoleRepository repository, IValidateData validateData, ISetData setData)
    {
        _tokenService = tokenService;
        _permissionRepository = permissionRepository;
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
    }

    public async Task<Role> Add(Role role, string token)
    {
        // Verify permissions.
        long userId = _tokenService.GetUserId(token);
        bool hasPermission = await _permissionRepository.HasPermission(userId, role.CompanyId, PermissionEnum.RoleCreate);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        // Set data and validate common data.
        _setData.SetRoleData(role: role, update: false);
        role.Permissions = await _permissionRepository.FindByCompanyIdAndUserId(
            companyId: role.CompanyId,
            userId: userId,
            permissionIds: role.Permissions.Select(p => p.Id).ToList()
        );
        await _validateData.ValidateRole(role: role, update: false);
        role.UserId = userId;
        return await _repository.SaveAsync(role);
    }

    public async Task<Role> Update(Role role, string token)
    {
        long userId = _tokenService.GetUserId(token);
        bool hasPermission = await _permissionRepository.HasPermission(userId, role.CompanyId, PermissionEnum.RoleUpdate);
        bool roleExist = await _repository.ExistsByIdAndCompanyId(role.Id, role.CompanyId);
        bool isEditable = await _repository.IsEditable(role.Id, role.CompanyId);
        if (!hasPermission || !roleExist || !isEditable) throw new ForbiddenException(ExceptionEnum.Forbidden);
        // Set data and validate common data.
        _setData.SetRoleData(role: role, update: true);
        role.Permissions = await _permissionRepository.FindByCompanyIdAndUserId(
            companyId: role.CompanyId,
            userId: userId,
            permissionIds: role.Permissions.Select(p => p.Id).ToList()
        );
        await _validateData.ValidateRole(role: role, update: true);
        role.UserId = userId;
        return await _repository.UpdateAsync(role);
    }
}
