using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Country>> GetAll(bool? active = null)
    {
        IEnumerable<Country> data = await _repository.FindAllAsync();
        data = data.OrderBy(c => c.Name);
        if (active == null) return data.ToList();
        if (active.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }

    public Task<Country?> GetByCode(string code)
    {
        return _repository.FindByCode(code);
    }
}
