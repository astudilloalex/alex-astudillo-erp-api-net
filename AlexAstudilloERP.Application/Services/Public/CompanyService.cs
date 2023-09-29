using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUserMembershipRepository _userMembershipRepository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;
    private readonly IRoleRepository _roleRepository;

    public CompanyService(ICompanyRepository repository, IPersonRepository personRepository,
        ISetData setData, IValidateData validateData, IPermissionRepository permissionRepository,
        IUserMembershipRepository userMembershipRepository, IRoleRepository roleRepository)
    {
        _repository = repository;
        _validateData = validateData;
        _setData = setData;
        _personRepository = personRepository;
        _permissionRepository = permissionRepository;
        _userMembershipRepository = userMembershipRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Company> AddAsync(Company company, string userCode)
    {
        await CanCreateCompany(userCode);
        company.UserCode = userCode;
        //company.UserId = _tokenService.GetUserId(token);
        //_setData.SetCompanyData(company);
        //await _validateData.ValidateCompany(company: company, update: false);
        //// Validate if person exists.
        //if (company.Person != null)
        //{
        //    _setData.SetPersonData(person: company.Person);
        //    Person? person = await _personRepository.FindByIdCard(company.Person.IdCard);
        //    if (person == null)
        //    {
        //        await _validateData.ValidatePerson(company.Person, update: false);
        //    }
        //    else
        //    {
        //        company.Person = null;
        //        company.PersonId = person.Id;
        //    }
        //}
        return await _repository.SaveAsync(company);
    }

    public async Task<Company?> GetByCode(string code, string userCode)
    {
        Company? finded = await _repository.FindByCode(code);
        if (finded == null) return finded;
        bool existsCompany = await _repository.ExistsByCompanyIdAndUserId(finded.Id, _tokenService.GetUserId(userCode));
        if (!existsCompany) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return finded;
    }

    public Task<IPage<Company>> GetAllAllowed(IPageable pageable, string token)
    {
        long userId = _tokenService.GetUserId(token);
        return _repository.FindByUserId(pageable, userId);
    }

    public async Task<Company> Update(Company company, string userCode)
    {
        long userId = _tokenService.GetUserId(userCode);
        bool permitted = await _permissionRepository.HasPermission(userId, company.Id, PermissionEnum.CompanyUpdate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        //company.UserId = userId;
        company.Active = true;
        _setData.SetCompanyData(company, update: true);
        //await _validateData.ValidateCompany(company: company, update: true);
        if (company.Person != null)
        {
            _setData.SetPersonData(company.Person, update: true);
            //await _validateData.ValidatePerson(company.Person, update: true);

        }
        return await _repository.UpdateAsync(company);
    }

    public async Task CanCreateCompany(string userCode)
    {
        List<Role> roles = await _roleRepository.FindOwnerByUserCode(userCode);
        List<UserMembership> memberships = await _userMembershipRepository.FindByUserCode(userCode);
        if (roles.Count > 0 && memberships.Any(m => m.MembershipId == (short)MembershipEnum.Free))
        {
            throw new ForbiddenException(ExceptionEnum.MembershipDoesNotAllowCreateCompany);
        }
    }

    public Task ValidateData(Company company, bool update = false)
    {
        if (company.Person != null)
        {

        }

    }
}
