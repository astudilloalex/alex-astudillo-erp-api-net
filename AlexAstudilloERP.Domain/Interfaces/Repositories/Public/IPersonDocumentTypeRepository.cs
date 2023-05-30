using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IPersonDocumentTypeRepository : INPRepository<PersonDocumentType, short>
{
    public Task<bool> IsActive(short id);
}
