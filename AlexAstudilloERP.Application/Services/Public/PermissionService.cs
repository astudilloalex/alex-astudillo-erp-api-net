using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class PermissionService(IPermissionRepository _repository) : IPermissionService
{
    public Task<IPage<Permission>> GetAll(IPageable pageable, int companyId, string token)
    {
        return _repository.FindAllAsync(pageable, companyId, 2);
    }
}
