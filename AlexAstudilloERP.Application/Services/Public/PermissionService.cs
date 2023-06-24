using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class PermissionService : IPermissionService
{
    private readonly ITokenService _tokenService;
    private readonly IPermissionRepository _repository;

    public PermissionService(ITokenService tokenService, IPermissionRepository repository)
    {
        _tokenService = tokenService;
        _repository = repository;
    }

    public Task<IPage<Permission>> GetAll(IPageable pageable, int companyId, string token)
    {
        throw new NotImplementedException();
    }
}
