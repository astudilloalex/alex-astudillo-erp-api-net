using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface ICustomerRepository : INPRepository<Customer, long>
{
    public Task<bool> ExistsByCompanyIdAndIdCardAsync(int companyId, string idCard);

    public Task<Customer?> FindByCodeAsync(string code);

    public Task<Customer?> FindByIdCard(string idCard);

    public Task<Customer?> FindByIdCardAndCompanyIdAsync(int companyId, string idCard);
}
