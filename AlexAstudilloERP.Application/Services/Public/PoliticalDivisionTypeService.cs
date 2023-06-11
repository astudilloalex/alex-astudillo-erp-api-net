using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class PoliticalDivisionTypeService : IPoliticalDivisionTypeService
{
    private readonly IPoliticalDivisionTypeRepository _repository;

    public PoliticalDivisionTypeService(IPoliticalDivisionTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PoliticalDivisionType>> GetAll(bool? onlyActive = null)
    {
        IEnumerable<PoliticalDivisionType> data = await _repository.FindAllAsync();
        if (onlyActive == null) return data.ToList();
        if (onlyActive.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }
}
