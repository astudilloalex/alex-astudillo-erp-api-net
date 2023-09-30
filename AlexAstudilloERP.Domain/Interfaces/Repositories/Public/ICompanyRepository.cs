using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface ICompanyRepository : INPRepository<Company, int>
{
    public Task<bool> ExistsByCompanyIdAndUserId(int companyId, long userId);

    public Task<bool> ExistsByPersonIdCard(string idCard);

    public Task<Company?> FindByCode(string code);

    public Task<Company?> FindByIdCardAsync(string idCard);

    public Task<IPage<Company>> FindByUserId(IPageable pageable, long userId);
}
