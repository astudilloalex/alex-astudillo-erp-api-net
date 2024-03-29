﻿using AlexAstudilloERP.Domain.Interfaces.Services.Custom;

namespace AlexAstudilloERP.Application.Services.Custom;

public class ValidateData : IValidateData
{
    //private readonly ICompanyRepository _companyRepository;
    //private readonly IEmailRepository _emailRepository;
    //private readonly IEstablishmentRepository _establishmentRepository;
    //private readonly IPersonRepository _personRepository;
    //private readonly IPoliticalDivisionRepository _politicalDivisionRepository;
    //private readonly IRoleRepository _roleRepository;
    //private readonly IUserRepository _userRepository;

    //public ValidateData(ICompanyRepository companyRepository, IEmailRepository emailRepository,
    //    IPersonRepository personRepository, IUserRepository userRepository,
    //    IEstablishmentRepository establishmentRepository, IPoliticalDivisionRepository politicalDivisionRepository,
    //    IRoleRepository roleRepository)
    //{
    //    _emailRepository = emailRepository;
    //    _personRepository = personRepository;
    //    _userRepository = userRepository;
    //    _companyRepository = companyRepository;
    //    _establishmentRepository = establishmentRepository;
    //    _politicalDivisionRepository = politicalDivisionRepository;
    //    _roleRepository = roleRepository;
    //}

    //public async Task ValidateAddress(Address address, bool update = false)
    //{
    //    if (address.MainStreet.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidMainStreet);
    //    short politicalDivisionTypeId = await _politicalDivisionRepository.FindTypeIdById(address.PoliticalDivisionId);
    //    if (politicalDivisionTypeId != (short)PoliticalDivisionTypeEnum.Canton) throw new InvalidFieldException(ExceptionEnum.InvalidPoliticalDivisionType);
    //}

    //public async Task ValidateCompany(Company company, bool update = false)
    //{
    //    if (company.Tradename.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidCompanyName);
    //    if (company.SpecialTaxpayer && company.SpecialTaxpayerNumber == null) throw new InvalidFieldException(ExceptionEnum.TaxpayerNumberIsRequired);
    //    Company? finded = null;
    //    if (company.Person != null) finded = await _companyRepository.FindByIdCard(company.Person.IdCard);
    //    if (update)
    //    {
    //        if (finded != null && finded.Id != company.Id) throw new UniqueKeyException(ExceptionEnum.AlreadyExistsCompanyWithThatIdCard);
    //    }
    //    else
    //    {
    //        if (finded != null) throw new UniqueKeyException(ExceptionEnum.AlreadyExistsCompanyWithThatIdCard);
    //    }
    //}

    //public async Task ValidateEmail(Email email, bool update = false)
    //{
    //    ValidateMail(email.Mail);
    //    if (!update)
    //    {
    //        email.Verified = false;
    //        if (await _emailRepository.ExistsMail(email.Mail)) throw new UniqueKeyException(ExceptionEnum.EmailAlreadyInUse);
    //    }
    //}

    //public async Task ValidateEstablishment(Establishment establishment, bool update = false)
    //{
    //    if (establishment.Name.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidEstablishmentName);
    //    if (!update)
    //    {
    //        if (establishment.Address == null) throw new InvalidFieldException(ExceptionEnum.EstablishmentAddressIsRequired);
    //        Establishment? finded = await _establishmentRepository.FindMainByCompanyId(establishment.CompanyId);
    //        if (finded == null && !establishment.Main)
    //        {
    //            throw new InvalidFieldException(ExceptionEnum.MainEstablishmentIsRequired);
    //        }
    //        else if (finded != null && establishment.Main)
    //        {
    //            throw new InvalidFieldException(ExceptionEnum.MainEstablishmentAlreadyExists);
    //        }
    //    }
    //}

    //public void ValidateMail(string mail)
    //{
    //    string pattern = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
    //    Regex reg = new(pattern);
    //    if (!reg.IsMatch(mail)) throw new InvalidFieldException(ExceptionEnum.InvalidEmail);
    //}

    //public void ValidatePassword(string password)
    //{
    //    if (password.Length < 8 || password.Length > 64) throw new InvalidFieldException(ExceptionEnum.InvalidPasswordLength);
    //    string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$";
    //    Regex reg = new(pattern);
    //    if (!reg.IsMatch(password)) throw new InvalidFieldException(ExceptionEnum.WeakPassword);
    //}

    //public async Task ValidatePerson(Person person, bool update = false)
    //{
    //    // Validate data.
    //    if ((person.FirstName == null || person.LastName == null) && person.SocialReason == null)
    //    {
    //        throw new InvalidFieldException(ExceptionEnum.InvalidPersonNames);
    //    }
    //    if (person.FirstName != null && person.FirstName.Length < 3) throw new InvalidFieldException(ExceptionEnum.InvalidFirstName);
    //    if (person.LastName != null && person.LastName.Length < 3) throw new InvalidFieldException(ExceptionEnum.InvalidLastName);
    //    if (person.SocialReason != null && person.SocialReason.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidSocialReason);
    //    if (!update)
    //    {
    //        if (await _personRepository.ExistsIdCard(person.IdCard)) throw new UniqueKeyException(ExceptionEnum.IdCardAlreadyExists);
    //    }
    //    else
    //    {

    //    }
    //}

    //public async Task ValidateRole(Role role, bool update = false)
    //{
    //    if (role.Permissions.Count < 1) throw new InvalidFieldException(ExceptionEnum.PermissionsAreRequired);
    //    if (update)
    //    {
    //        Role? finded = await _roleRepository.FindByNameAndCompanyId(role.CompanyId, role.Name);
    //        if (finded != null && finded.Id != role.Id)
    //        {
    //            throw new InvalidFieldException(ExceptionEnum.RoleNameAlreadyExists);
    //        }
    //    }
    //    else
    //    {
    //        if (await _roleRepository.ExistsByNameAndCompanyId(role.CompanyId, role.Name))
    //        {
    //            throw new InvalidFieldException(ExceptionEnum.RoleNameAlreadyExists);
    //        }
    //    }
    //}

    //public async Task ValidateUser(User user, bool update = false)
    //{
    //    if (!update)
    //    {
    //        if (await _userRepository.ExistsUsername(user.Username ?? "")) throw new UniqueKeyException(ExceptionEnum.UsernameAlreadyExist);
    //    }
    //}
}
