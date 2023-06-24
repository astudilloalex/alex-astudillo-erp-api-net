using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IPermissionService
{
    public Task<IPage<Permission>> GetAll(IPageable pageable, int companyId, string token);
}
