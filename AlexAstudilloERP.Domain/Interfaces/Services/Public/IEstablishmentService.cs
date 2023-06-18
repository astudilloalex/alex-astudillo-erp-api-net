using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IEstablishmentService
{
    public Task<Establishment> Add(Establishment establishment, string token);

    public Task<IPage<Establishment>> FindByCompanyId(IPageable pageable, int companyId, string token);

    public Task<Establishment?> GetByCode(string code, string token);

    public Task<Establishment> SetMain(int id, string token);

    public Task<Establishment> Update(Establishment establishment, string token);
}
