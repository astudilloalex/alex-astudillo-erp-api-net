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

    public RoleService(ITokenService tokenService, IPermissionRepository permissionRepository, IRoleRepository repository)
    {
        _tokenService = tokenService;
        _permissionRepository = permissionRepository;
        _repository = repository;
    }

    public async Task<Role> Add(Role role, string token)
    {
        long userId = _tokenService.GetUserId(token);
        bool hasPermission = await _permissionRepository.HasPermission(userId, role.CompanyId, PermissionEnum.RoleCreate);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        throw new NotImplementedException();
    }

    public async Task<Role> Update(Role role, string token)
    {
        long userId = _tokenService.GetUserId(token);
        bool hasPermission = await _permissionRepository.HasPermission(userId, role.CompanyId, PermissionEnum.RoleUpdate);
        if (!hasPermission) throw new ForbiddenException(ExceptionEnum.Forbidden);
        throw new NotImplementedException();
    }
}
