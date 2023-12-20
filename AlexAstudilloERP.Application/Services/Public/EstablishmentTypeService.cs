using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class EstablishmentTypeService(IEstablishmentTypeRepository _repository) : IEstablishmentTypeService
{
    public Task<List<EstablishmentType>> GetAll()
    {
        return _repository.FindAllActives();
    }
}
