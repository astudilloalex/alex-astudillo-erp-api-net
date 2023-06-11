using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IPoliticalDivisionService
{
    public Task<List<PoliticalDivision>> GetByParentId(int parentId, bool? active = null);
    public Task<List<PoliticalDivision>> GetByTypeId(short type, bool? active = null);
    public Task<List<PoliticalDivision>> GetByTypeIdAndCountryId(short countryId, short typeId, bool? active = null);
}
