using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface ICustomerService
{
    public Task<Customer?> GetByIdCard(string idCard, string token);
    public Task<Customer?> GetByIdCardAndCompanyId(int companyId, string idCard, string token);
}
