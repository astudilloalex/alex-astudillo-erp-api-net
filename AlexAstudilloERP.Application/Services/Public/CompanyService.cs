using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IPersonRepository _personRepository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public CompanyService(ICompanyRepository repository, IPersonRepository personRepository, ISetData setData, IValidateData validateData)
    {
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
        _personRepository = personRepository;
    }

    public async Task<Company> AddAsync(Company company, string token)
    {
        _setData.SetCompanyData(company);
        _validateData.ValidateCompany(company: company, update: false);
        // Validate all establishments.
        for (int i = 0; i < company.Establishments.Count; i++)
        {
            Establishment establishment = company.Establishments.ElementAt(i);
            _setData.SetEstablishmentData(establishment, false);
            if (establishment.Address != null)
            {
                _setData.SetAddressData(establishment.Address, false);
                _validateData.ValidateAddress(establishment.Address, false);
            }
            _validateData.ValidateEstablishment(establishment: establishment, update: false);
        }
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
                person.JuridicalPerson = company.Person.JuridicalPerson;
                person.DocumentTypeId = company.Person.DocumentTypeId;
                person.Birthdate = company.Person.Birthdate;
                person.FirstName = company.Person.FirstName;
                person.LastName = company.Person.LastName;
                person.SocialReason = company.Person.SocialReason;
                person.GenderId = company.Person.GenderId;
                await _personRepository.UpdateAsync(person);
                company.Person = null;
                company.PersonId = person.Id;
            }
        }
        return await _repository.SaveAsync(company);
    }
}
