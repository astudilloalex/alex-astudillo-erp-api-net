using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IMenuRepository
{
    public Task<List<Menu>> FindByUserCodeAndCompanyCodeAsync(string userCode, string companyCode);

    public Task<List<Menu>> FindParentsAsync(string userCode, string companyCode, bool? isPublic = null);
}
