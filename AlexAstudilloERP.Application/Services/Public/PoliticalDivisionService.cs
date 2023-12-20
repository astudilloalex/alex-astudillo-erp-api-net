using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class PoliticalDivisionService(IPoliticalDivisionRepository _repository) : IPoliticalDivisionService
{
    public async Task<List<PoliticalDivision>> GetByParentId(int parentId, bool? active = null)
    {
        List<PoliticalDivision> data = await _repository.FindByParentId(parentId);
        if (active == null) return data;
        if (active.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }

    public async Task<List<PoliticalDivision>> GetByTypeId(short typeId, bool? active = null)
    {
        List<PoliticalDivision> data = await _repository.FindByTypeId(typeId);
        if (active == null) return data;
        if (active.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }

    public async Task<List<PoliticalDivision>> GetByTypeIdAndCountryId(short countryId, short typeId, bool? active = null)
    {
        List<PoliticalDivision> data = await _repository.FindByTypeIdAndCountryId(countryId, typeId);
        if (active == null) return data;
        if (active.Value) return data.Where(p => p.Active).ToList();
        return data.Where(p => !p.Active).ToList();
    }
}
