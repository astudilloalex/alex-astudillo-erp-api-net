using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface ICompanyService
{
    public Task<Company> AddAsync(Company company, string userCode);

    public Task<Company?> GetByCode(string code, string userCode);

    public Task<IPage<Company>> GetAllAllowed(IPageable pageable, string userCode);

    public Task<Company> Update(Company company, string userCode);
}
