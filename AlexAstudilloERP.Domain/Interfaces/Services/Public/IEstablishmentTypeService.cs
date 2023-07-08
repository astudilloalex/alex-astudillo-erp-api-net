using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IEstablishmentTypeService
{
    public Task<List<EstablishmentType>> GetAll();
}
