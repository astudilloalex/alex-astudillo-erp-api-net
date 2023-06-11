using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IPoliticalDivisionTypeService
{
    public Task<List<PoliticalDivisionType>> GetAll(bool? onlyActive = null);
}
