using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IPersonRepository _personRepository;
    private readonly ITokenService _tokenService;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public CompanyService(ICompanyRepository repository, IPersonRepository personRepository,
        ITokenService tokenService, ISetData setData,
        IValidateData validateData)
    {
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
        _personRepository = personRepository;
        _tokenService = tokenService;
    }

    public async Task<Company> AddAsync(Company company, string token)
    {
        company.UserId = _tokenService.GetUserId(token);
        _setData.SetCompanyData(company);
        await _validateData.ValidateCompany(company: company, update: false);
        // Validate if person exists.
        if (company.Person != null)
        {
            _setData.SetPersonData(person: company.Person);
            Person? person = await _personRepository.FindByIdCard(company.Person.IdCard);
            if (person == null)
            {
                await _validateData.ValidatePerson(company.Person, update: false);
            }
            else
            {
                company.Person = null;
                company.PersonId = person.Id;
            }
        }
        return await _repository.SaveAsync(company);
    }
}
