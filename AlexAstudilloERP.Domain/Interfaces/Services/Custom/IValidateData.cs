using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.BadRequest;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public void ValidateIdCard(string countryCode, string idCard, bool juridicalPerson)
    {
        switch (countryCode)
        {
            case "EC":
                ValidateEcuadorianIdCard(idCard, juridicalPerson);
                break;
        }
    }

    /// <summary>
    /// Validate a Ecuadorian Id Card.
    /// </summary>
    /// <param name="idCard">The id card to validate.</param>
    /// <param name="juridicalPerson"></param>
    /// <exception cref="InvalidFieldException"></exception>
    public void ValidateEcuadorianIdCard(string idCard, bool juridicalPerson)
    {
        if (idCard.Length != 10 && idCard.Length != 13) throw new InvalidFieldException(ExceptionEnum.InvalidIdCardLength);
        char[] chars = idCard.ToCharArray();
        byte sum = 0;
        List<byte> coefficients = new();
        try
        {
            byte verifier = byte.Parse($"{chars[0]}{chars[1]}");
            if (verifier < 1 || verifier > 24) throw new InvalidFieldException(ExceptionEnum.InvalidProvinceCode);
            byte[] digits = new byte[chars.Length];
            for (byte i = 0; i < digits.Length; i++) digits[i] = byte.Parse($"{chars[i]}");
            // Validate a juridical person.
            if (juridicalPerson)
            {
                if (digits[2] != 6 && digits[2] != 9) throw new InvalidFieldException(ExceptionEnum.InvalidIdCard);
                if (digits[2] == 6) coefficients = new() { 3, 2, 7, 6, 5, 4, 3, 2 };
                if (digits[2] == 9) coefficients = new() { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                for (byte i = 0; i < coefficients.Count; i++) sum += (byte)(coefficients[i] * digits[i]);
                if (sum % 11 == 0 && (digits[2] == 6 ? digits[8] : digits[9]) != 0) throw new InvalidFieldException(ExceptionEnum.InvalidVerificationDigit);
                if (11 - (sum % 11) != (digits[2] == 6 ? digits[8] : digits[9])) throw new InvalidFieldException(ExceptionEnum.InvalidVerificationDigit);
            }
            // Validate a natural person
            else
            {
                if (digits[2] > 6) throw new InvalidFieldException(ExceptionEnum.InvalidIdCard);
                coefficients = new() { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
                for (byte i = 0; i < coefficients.Count; i++)
                {
                    byte total = (byte)(coefficients[i] * digits[i]);
                    if (total >= 10) total -= 9;
                    sum += total;
                }
                if (sum % 10 == 0 && digits[9] != 0) throw new InvalidFieldException(ExceptionEnum.InvalidVerificationDigit);
                if (10 - (sum % 10) != digits[9]) throw new InvalidFieldException(ExceptionEnum.InvalidVerificationDigit);
            }
        }
        catch
        {
            throw new InvalidFieldException(ExceptionEnum.InvalidIdCard);
        }
    }
    //public Task ValidatePerson(Person person, bool update = false);
    //public Task ValidateRole(Role role, bool update = false);
    //public Task ValidateUser(User user, bool update = false);
}
