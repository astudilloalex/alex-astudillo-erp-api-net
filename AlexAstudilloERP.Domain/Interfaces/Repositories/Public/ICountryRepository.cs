using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface ICountryRepository : INPRepository<Country, short>
{
    public Task<Country?> FindByCode(string code);

    public Task<string?> FindCodeByPersonDocumentTypeId(short documentTypeId);
}
