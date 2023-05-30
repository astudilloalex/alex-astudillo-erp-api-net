using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.BadRequest;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using System.Text.RegularExpressions;

namespace AlexAstudilloERP.Application.Services.Custom
{
    public class ValidateData : IValidateData
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;

        public ValidateData(IEmailRepository emailRepository, IPersonRepository personRepository, IUserRepository userRepository)
        {
            _emailRepository = emailRepository;
            _personRepository = personRepository;
            _userRepository = userRepository;
        }

        public void ValidateAddress(Address address, bool update = false)
        {
            if (address.MainStreet.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidMainStreet);
        }

        public void ValidateCompany(Company company, bool update = false)
        {
            if (company.Tradename.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidCompanyName);
            if (!update)
            {
                if (company.Establishments.Count(e => e.Main) != 1) throw new InvalidFieldException(ExceptionEnum.MainEstablishmentIsRequired);
            }
        }

        public async Task ValidateEmail(Email email, bool update = false)
        {
            ValidateMail(email.Mail);
            if (!update)
            {
                email.Verified = false;
                if (await _emailRepository.ExistsMail(email.Mail)) throw new UniqueKeyException(ExceptionEnum.EmailAlreadyInUse);
            }
        }

        public void ValidateEstablishment(Establishment establishment, bool update = false)
        {
            if (establishment.Name.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidEstablishmentName);
            if (!update)
            {
                if (establishment.Address == null) throw new InvalidFieldException(ExceptionEnum.EstablishmentAddressIsRequired);
            }
        }

        public void ValidateMail(string mail)
        {
            string pattern = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            Regex reg = new(pattern);
            if (!reg.IsMatch(mail)) throw new InvalidFieldException(ExceptionEnum.InvalidEmail);
        }

        public void ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 64) throw new InvalidFieldException(ExceptionEnum.InvalidPasswordLength);
            string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$";
            Regex reg = new(pattern);
            if (!reg.IsMatch(password)) throw new InvalidFieldException(ExceptionEnum.WeakPassword);
        }

        public async Task ValidatePerson(Person person, bool update = false)
        {
            // Validate data.
            if ((person.FirstName == null || person.LastName == null) && person.SocialReason == null)
            {
                throw new InvalidFieldException(ExceptionEnum.InvalidPersonNames);
            }
            if (person.FirstName != null && person.FirstName.Length < 3) throw new InvalidFieldException(ExceptionEnum.InvalidFirstName);
            if (person.LastName != null && person.LastName.Length < 3) throw new InvalidFieldException(ExceptionEnum.InvalidLastName);
            if (person.SocialReason != null && person.SocialReason.Length < 4) throw new InvalidFieldException(ExceptionEnum.InvalidSocialReason);
            if (!update)
            {
                if (await _personRepository.ExistsIdCard(person.IdCard)) throw new UniqueKeyException(ExceptionEnum.IdCardAlreadyExists);
            }
            else
            {

            }
        }

        public async Task ValidateUser(User user, bool update = false)
        {
            if (!update)
            {
                if (await _userRepository.ExistsUsername(user.Username)) throw new UniqueKeyException(ExceptionEnum.UsernameAlreadyExist);
            }
        }
    }
}
