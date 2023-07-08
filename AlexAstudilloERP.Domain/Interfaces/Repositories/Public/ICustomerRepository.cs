using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface ICustomerRepository : INPRepository<Customer, long>
{
    public Task<Customer?> FindByIdCard(string idCard);

    public Task<Customer?> FindByIdCardAndCompanyId(int companyId, string idCard);
}
