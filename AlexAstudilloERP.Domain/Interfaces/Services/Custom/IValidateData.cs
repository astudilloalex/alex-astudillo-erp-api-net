using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.BadRequest;
using System.Text.RegularExpressions;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface IValidateData
{
    //public Task ValidateAddress(Address address, bool update = false);
    //public Task ValidateCompany(Company company, bool update = false);
    //public Task ValidateEmail(Email email, bool update = false);
    //public Task ValidateEstablishment(Establishment establishment, bool update = false);
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
    //public Task ValidatePerson(Person person, bool update = false);
    //public Task ValidateRole(Role role, bool update = false);
    //public Task ValidateUser(User user, bool update = false);
}
