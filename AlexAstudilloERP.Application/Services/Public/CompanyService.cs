using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly ICountryRepository _countryRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUserMembershipRepository _userMembershipRepository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;
    private readonly IRoleRepository _roleRepository;

    public CompanyService(ICompanyRepository repository, IPersonRepository personRepository,
        ISetData setData, IValidateData validateData, IPermissionRepository permissionRepository,
        IUserMembershipRepository userMembershipRepository, IRoleRepository roleRepository, ICountryRepository countryRepository)
    {
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
        _personRepository = personRepository;
        _permissionRepository = permissionRepository;
        _userMembershipRepository = userMembershipRepository;
        _roleRepository = roleRepository;
        _countryRepository = countryRepository;
    }

    public async Task<Company> AddAsync(Company company, string userCode)
    {
        await CanCreateCompany(userCode);
        _setData.SetCompanyData(company);
        await ValidateData(company);
        company.UserCode = userCode;
        return await _repository.SaveAsync(company);
    }

    public async Task<Company?> GetByCode(string code, string userCode)
    {
        Company? finded = await _repository.FindByCode(code);
        if (finded == null) return finded;
        bool existsCompany = await _repository.ExistsByCompanyIdAndUserId(finded.Id, 1L);
        if (!existsCompany) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return finded;
    }

    public Task<IPage<Company>> GetAllAllowed(IPageable pageable, string token)
    {
        return _repository.FindByUserId(pageable, 1L);
    }

    public async Task<Company> Update(Company company, string userCode)
    {
        bool permitted = await _permissionRepository.HasPermission(userCode, company.Code, PermissionEnum.CompanyUpdate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        company.Person = null;
        _setData.SetCompanyData(company);
        await ValidateData(company);
        company.UserCode = userCode;
        company.Active = true;
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
                company.Person.JuridicalPerson
            );
        }
        _validateData.ValidateCompany(company);
        if (update)
        {
        }
        else
        {
            if (await _repository.ExistsByPersonIdCard(company.Person?.IdCard ?? "")) throw new UniqueKeyException(ExceptionEnum.AlreadyExistsCompanyWithThatIdCard);
        }
    }
}
