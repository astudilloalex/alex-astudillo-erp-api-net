using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface ICompanyService
{
    public Task<Company> AddAsync(Company company, string token);

    public Task<Company?> GetByCode(string code, string token);

    public Task<Company> Update(Company company, string token);    
}
