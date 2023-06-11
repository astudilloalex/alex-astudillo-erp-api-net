using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class EstablishmentService : IEstablishmentService
{
    private readonly IEstablishmentRepository _repository;

    public EstablishmentService(IEstablishmentRepository repository)
    {
        _repository = repository;
    }

    public Task<Establishment> Add(Establishment establishment, string token)
    {
        throw new NotImplementedException();
    }
}
