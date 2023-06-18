using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IEstablishmentRepository : INPRepository<Establishment, int>
{
    public Task<Establishment?> FindByCode(string code);

    public Task<IPage<Establishment>> FindByCompanyId(IPageable pageable, int companyId);

    public Task<Establishment?> FindMainByCompanyId(int companyId);

    public Task<Establishment> SetMain(int id, long userId);
}
