using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IEstablishmentService
{
    public Task<Establishment> Add(Establishment establishment, string token);
}
