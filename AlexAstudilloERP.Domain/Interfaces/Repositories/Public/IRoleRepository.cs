using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IRoleRepository : INPRepository<Role, int>
{
    public Task<bool> ExistsName(int companyId, string name);
}
