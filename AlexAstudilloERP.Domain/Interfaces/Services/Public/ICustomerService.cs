using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface ICustomerService
{
    public Task<Customer> Add(Customer customer, string userCode, string companyCode);

    public Task<Customer?> GetByCodeAsync(string code, string userCode, string companyCode);

    public Task<Customer?> GetByIdCard(string idCard, string userCode, string companyCode);

    public Task<Customer?> GetByIdCardAndCompanyId(int companyId, string idCard, string userCode);

    public Task<Customer> UpdateAsync(Customer customer, string userCode, string companyCode);
}
