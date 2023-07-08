using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IEstablishmentTypeRepository : INPRepository<EstablishmentType, short>
{
    public Task<List<EstablishmentType>> FindAllActives();
}
