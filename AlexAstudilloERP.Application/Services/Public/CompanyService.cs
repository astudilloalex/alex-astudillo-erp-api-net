using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IValidateData _validateData;

    public CompanyService(ICompanyRepository repository, IValidateData validateData)
    {
        _repository = repository;
        _validateData = validateData;
    }

    public Task<Company> AddAsync(Company company, string token)
    {

        throw new NotImplementedException();
    }
}
