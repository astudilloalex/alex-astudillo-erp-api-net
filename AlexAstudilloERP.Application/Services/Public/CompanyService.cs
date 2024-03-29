﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService(
    ICompanyRepository _repository, 
    IPersonRepository _personRepository,
    ISetData _setData, 
    IValidateData _validateData, 
    IPermissionRepository _permissionRepository,
    IUserMembershipRepository _userMembershipRepository, 
    IRoleRepository _roleRepository, 
    ICountryRepository _countryRepository
) : ICompanyService
{
    public async Task<Company> AddAsync(Company company, string userCode)
    {
        await CanCreateCompany(userCode);
        _setData.SetCompanyData(company);
        await ValidateData(company);
        Person person = await _personRepository.SaveOrUpdate(company.Person!);
        company.Person = null;
        company.PersonId = person.Id;
        company.UserCode = userCode;
        return await _repository.SaveAsync(company);
    }

    public async Task<Company?> GetByCode(string code, string userCode)
    {
        bool permitted = await _permissionRepository.HasPermission(userCode, code, PermissionEnum.CompanyList);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByCodeAsync(code);
    }

    public Task<IPage<Company>> GetAllAllowed(IPageable pageable, string userCode)
    {
        return _repository.FindByUserCodeAsync(pageable, userCode);
    }

    public async Task<Company> Update(Company company, string userCode)
    {
        bool permitted = await _permissionRepository.HasPermission(userCode, company.Code, PermissionEnum.CompanyUpdate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetCompanyData(company);
        await ValidateData(company);
        Person person = await _personRepository.SaveOrUpdate(company.Person!);
        company.Person = null;
        company.PersonId = person.Id;
        company.UserCode = userCode;
        return await _repository.UpdateAsync(company);
    }

    public async Task CanCreateCompany(string userCode)
    {
        int ownerRoles = await _roleRepository.CountOwnerByUserCode(userCode);
        List<UserMembership> memberships = await _userMembershipRepository.FindByUserCode(userCode);
        if ((ownerRoles > 0 && memberships.Any(m => m.MembershipId == (short)MembershipEnum.Free)) || memberships.Count == 0)
        {
            throw new ForbiddenException(ExceptionEnum.MembershipDoesNotAllowCreateCompany);
        }
    }

    public async Task ValidateData(Company company, bool update = false)
    {
        if (company.Person != null)
        {
            _validateData.ValidateIdCard(
                await _countryRepository.FindCodeByPersonDocumentTypeId(company.Person.PersonDocumentTypeId) ?? "",
                company.Person.IdCard,
                company.Person.JuridicalPerson,
                (PersonDocumentTypeEnum)company.Person.PersonDocumentTypeId
            );
        }
        _validateData.ValidateCompany(company);
        if (update)
        {
            Company? finded = await _repository.FindByIdCardAsync(company.Person?.IdCard ?? "");
            if (finded != null && company.Person?.IdCard != finded.Person?.IdCard) throw new UniqueKeyException(ExceptionEnum.AlreadyExistsCompanyWithThatIdCard);
        }
        else
        {
            if (await _repository.ExistsByPersonIdCard(company.Person?.IdCard ?? "")) throw new UniqueKeyException(ExceptionEnum.AlreadyExistsCompanyWithThatIdCard);
        }
    }
}
