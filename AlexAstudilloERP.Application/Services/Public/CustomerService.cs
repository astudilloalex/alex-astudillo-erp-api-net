using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class CustomerService(
    ICustomerRepository _repository, 
    IPermissionRepository _permissionRepository, 
    ICompanyRepository _companyRepository,
    IPersonRepository _personRepository, 
    ISetData _setData, 
    IValidateData _validateData, 
    ICountryRepository _countryRepository
) : ICustomerService
{
    public async Task<Customer> Add(Customer customer, string userCode, string companyCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, companyCode, PermissionEnum.CustomerCreate);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetCustomerData(customer);
        await ValidateData(customer);
        Person person = await _personRepository.FindByIdCard(customer.Person!.IdCard ?? "") ?? await _personRepository.SaveAsync(customer.Person!);
        customer.Person = null;
        customer.PersonId = person.Id;
        customer.UserCode = userCode;
        customer.CompanyId = await _companyRepository.FindIdByCodeAsync(companyCode);
        return await _repository.SaveAsync(customer);
    }

    public async Task<Customer> ChangeStateAsync(Customer customer, string userCode, string companyCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, companyCode, customer.Active ? PermissionEnum.CustomerEnable : PermissionEnum.CustomerDisable);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        if (!await _repository.ExistsByCompanyCodeAndCode(companyCode, customer.Code)) throw new ForbiddenException(ExceptionEnum.Forbidden);
        customer.UserCode = userCode;
        return await _repository.ChangeStateAsync(customer);
    }

    public async Task<Customer?> GetByCodeAsync(string code, string userCode, string companyCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, companyCode, PermissionEnum.CustomerCreate);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        Customer? finded = await _repository.FindByCodeAsync(code);
        if (finded == null) return finded;
        if (finded.CompanyId != await _companyRepository.FindIdByCodeAsync(companyCode)) return null;
        return finded;
    }

    public async Task<Customer?> GetByIdCard(string idCard, string userCode, string companyCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, companyCode, PermissionEnum.CustomerList);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByIdCardAndCompanyCodeAsync(idCard, companyCode);
    }

    public Task<Customer?> GetByIdCardAndCompanyId(int companyId, string idCard, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        //bool permited = await _permissionRepository.HasPermission(1, companyId, PermissionEnum.CustomerGet);
        //if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        //return await _repository.FindByIdCardAndCompanyCodeAsync(companyId, idCard);
        throw new NotImplementedException();
    }

    public async Task<Customer> UpdateAsync(Customer customer, string userCode, string companyCode)
    {
        bool permited = await _permissionRepository.HasPermission(userCode, companyCode, PermissionEnum.CustomerUpdate);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        if (!await _repository.ExistsByCompanyCodeAndCode(companyCode, customer.Code)) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetCustomerData(customer);
        await ValidateData(customer, true);
        Person person = await _personRepository.FindByIdCard(customer.Person!.IdCard ?? "") ?? await _personRepository.SaveAsync(customer.Person!);
        customer.Person = null;
        customer.PersonId = person.Id;
        customer.UserCode = userCode;
        customer.CompanyId = await _companyRepository.FindIdByCodeAsync(companyCode);
        return await _repository.UpdateAsync(customer);
    }

    public async Task ValidateData(Customer customer, bool update = false)
    {
        if (customer.Person != null)
        {
            _validateData.ValidateIdCard(
               await _countryRepository.FindCodeByPersonDocumentTypeId(customer.Person.PersonDocumentTypeId) ?? "",
               customer.Person.IdCard,
               customer.Person.JuridicalPerson,
               (PersonDocumentTypeEnum)customer.Person.PersonDocumentTypeId
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
