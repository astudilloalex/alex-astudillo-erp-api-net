﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPersonRepository _personRepository;
    private readonly ICountryRepository _countryRepository;

    public CustomerService(ICustomerRepository repository, IPermissionRepository permissionRepository, ICompanyRepository companyRepository,
        IPersonRepository personRepository, ISetData setData, IValidateData validateData, ICountryRepository countryRepository)
    {
        _repository = repository;
        _permissionRepository = permissionRepository;
        _companyRepository = companyRepository;
        _personRepository = personRepository;
        _setData = setData;
        _validateData = validateData;
        _countryRepository = countryRepository;
    }

    public async Task<Customer> Add(Customer customer, string userCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, await _companyRepository.FindCodeById(customer.CompanyId) ?? "", PermissionEnum.CustomerCreate);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetCustomerData(customer);
        await ValidateData(customer);
        Person person = await _personRepository.FindByIdCard(customer.Person!.IdCard ?? "") ?? await _personRepository.SaveAsync(customer.Person!);
        customer.Person = null;
        customer.PersonId = person.Id;
        customer.UserCode = userCode;
        return await _repository.SaveAsync(customer);
    }

    public Task<Customer?> GetByIdCard(string idCard, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByIdCardAndCompanyId(int companyId, string idCard, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        //bool permited = await _permissionRepository.HasPermission(1, companyId, PermissionEnum.CustomerGet);
        //if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByIdCardAndCompanyIdAsync(companyId, idCard);
    }

    public async Task ValidateData(Customer customer, bool update = false)
    {
        if (customer.Person != null)
        {
            _validateData.ValidateIdCard(
               await _countryRepository.FindCodeByPersonDocumentTypeId(customer.Person.PersonDocumentTypeId) ?? "",
               customer.Person.IdCard,
               customer.Person.JuridicalPerson
           );
        }
        _validateData.ValidateCustomer(customer);
        if (update)
        {

        }
        else
        {
            if (await _repository.ExistsByCompanyIdAndIdCardAsync(customer.CompanyId, customer.Person!.IdCard))
            {
                throw new UniqueKeyException(ExceptionEnum.CustomerAlreadyExists);
            }
        }
    }
}
