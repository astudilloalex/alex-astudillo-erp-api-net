using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IMenuService
{
    public Task<List<Menu>> GetByUserCodeAndCompanyCodeAsync(string userCode, string companyCode);
}
