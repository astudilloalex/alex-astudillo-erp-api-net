using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface ICountryService
{
    public Task<List<Country>> GetAll(bool? active = null);
    public Task<Country?> GetByCode(string code);
}
