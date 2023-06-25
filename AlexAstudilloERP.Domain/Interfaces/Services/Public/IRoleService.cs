using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IRoleService
{
    public Task<Role> Add(Role role, string token);
    public Task<IPage<Role>> GetAll(IPageable pageable, int companyId, string token, bool? active = null);
    public Task<Role> Update(Role role, string token);
}
