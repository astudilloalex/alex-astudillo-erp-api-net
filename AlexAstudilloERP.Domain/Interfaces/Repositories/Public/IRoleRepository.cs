using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IRoleRepository : INPRepository<Role, int>
{
    public Task<bool> ExistsByNameAndCompanyId(int companyId, string name);

    public Task<Role?> FindByNameAndCompanyId(int companyId, string name);
}
