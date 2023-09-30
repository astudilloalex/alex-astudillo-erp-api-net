using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.BadRequest;
using System;
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

    public void ValidateIdCard(string countryCode, string idCard, bool juridicalPerson)
    {
        switch (countryCode)
        {
            case "EC":
                ValidateEcuadorianIdCard(idCard, juridicalPerson);
                break;
            default:
                throw new InvalidFieldException(ExceptionEnum.InvalidIdCard);
        }
    }

    public void ValidateCompany(Company company)
    {
        if (company.Tradename.Length < 3) throw new InvalidFieldException(ExceptionEnum.InvalidCompanyName);
        if (company.Person != null) ValidatePerson(company.Person);
    }

    public void ValidateCustomer(Customer customer)
    {
        if (customer.JuridicalPerson && (customer.SocialReason == null || customer.SocialReason.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidSocialReason);
        if (!customer.JuridicalPerson && (customer.FirstName == null || customer.FirstName.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidFirstName);
        if (!customer.JuridicalPerson && (customer.LastName == null || customer.LastName.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidLastName);
        if (customer.Birthdate != null && customer.Birthdate > DateTime.UtcNow.AddYears(-15)) throw new InvalidFieldException(ExceptionEnum.InvalidBirthdate);
    }

    public void ValidatePerson(Person person)
    {
        if (person.JuridicalPerson && (person.SocialReason == null || person.SocialReason.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidSocialReason);
        if (!person.JuridicalPerson && (person.FirstName == null || person.FirstName.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidFirstName);
        if (!person.JuridicalPerson && (person.LastName == null || person.LastName.Length < 3)) throw new InvalidFieldException(ExceptionEnum.InvalidLastName);
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
            if (verifier < 1 || verifier > 24) throw new Exception();
            byte[] digits = new byte[chars.Length];
            for (byte i = 0; i < digits.Length; i++) digits[i] = byte.Parse($"{chars[i]}");
            if (digits.Length == 13 && short.Parse($"{digits[10]}{digits[11]}{digits[12]}") < 1) throw new Exception();
            verifier = digits[9];
            // Validate a juridical person.
            if (juridicalPerson)
            {
                if (digits[2] != 6 && digits[2] != 9) throw new Exception();
                if (digits[2] == 6)
                {
                    coefficients = new() { 3, 2, 7, 6, 5, 4, 3, 2 };
                    verifier = digits[8];
                }
                if (digits[2] == 9) coefficients = new() { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                for (byte i = 0; i < coefficients.Count; i++) sum += (byte)(coefficients[i] * digits[i]);
                if (sum % 11 == 0 && verifier != 0) throw new Exception();
                if (11 - (sum % 11) != verifier) throw new Exception();
            }
            // Validate a natural person
            else
            {
                if (digits[2] > 6) throw new Exception();
                coefficients = new() { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
                for (byte i = 0; i < coefficients.Count; i++)
                {
                    byte total = (byte)(coefficients[i] * digits[i]);
                    if (total >= 10) total -= 9;
                    sum += total;
                }
                if (sum % 10 == 0 && verifier != 0) throw new Exception();
                if (10 - (sum % 10) != verifier) throw new Exception();
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
