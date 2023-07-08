using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class EstablishmentTypeService : IEstablishmentTypeService
{
    private readonly IEstablishmentTypeRepository _repository;

    public EstablishmentTypeService(IEstablishmentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<EstablishmentType>> GetAll()
    {
        return _repository.FindAllActives();
    }
}
