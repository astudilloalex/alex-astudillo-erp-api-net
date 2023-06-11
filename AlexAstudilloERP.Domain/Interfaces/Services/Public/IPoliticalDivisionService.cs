using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IPoliticalDivisionService
{
    public Task<List<PoliticalDivision>> GetByParentId(int parentId, bool? onlyActive = null);
    public Task<List<PoliticalDivision>> GetByTypeId(short type, bool? onlyActive = null);
}
