using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class PoliticalDivisionService : IPoliticalDivisionService
{
    private readonly IPoliticalDivisionRepository _repository;

    public PoliticalDivisionService(IPoliticalDivisionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PoliticalDivision>> GetByParentId(int parentId, bool? onlyActive = null)
    {
        List<PoliticalDivision> data = await _repository.FindByParentId(parentId);
        if (onlyActive == null) return data.ToList();
        if (onlyActive.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }

    public async Task<List<PoliticalDivision>> GetByTypeId(short typeId, bool? onlyActive = null)
    {
        List<PoliticalDivision> data = await _repository.FindByTypeId(typeId);
        if (onlyActive == null) return data.ToList();
        if (onlyActive.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }
}
