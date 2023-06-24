using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface ISetData
{
    public void SetAddressData(Address address, bool update = false)
    {
        address.MainStreet = address.MainStreet.Trim().ToUpperInvariant();
        address.SecondaryStreet = address.SecondaryStreet?.Trim().ToUpperInvariant();
    }

    public void SetCompanyData(Company company, bool update = false)
    {
        company.Parent = null;
        company.Tradename = company.Tradename.Trim().ToUpperInvariant();
    }

    public void SetEstablishmentData(Establishment establishment, bool update = false)
    {
        establishment.Name = establishment.Name.Trim().ToUpperInvariant();
        establishment.Description = establishment.Description?.Trim()?.ToUpperInvariant();
    }

    public void SetEmailData(Email email, bool update = false)
    {
        email.Mail = email.Mail.Trim();
        if (!update) email.Verified = false;
    }

    public void SetPersonData(Person person, bool update = false)
    {
        person.IdCard = person.IdCard.Trim().ToUpper();
        person.FirstName = person.JuridicalPerson ? null : person.FirstName?.Trim().ToUpperInvariant();
        person.LastName = person.JuridicalPerson ? null : person.LastName?.Trim().ToUpperInvariant();
        person.SocialReason = person.JuridicalPerson ? person.SocialReason?.Trim().ToUpperInvariant() : null;
        person.Addresses = new List<Address>();
        person.Emails = new List<Email>();
        person.Phones = new List<Phone>();
        person.DocumentType = null;
        person.Gender = null;
    }

    public void SetRoleData(Role role, bool update = false)
    {
        role.Name = role.Name.Trim().ToUpperInvariant();
        role.Description = role.Description?.Trim().ToUpperInvariant();
    }

    public void SetUserData(User user, bool update = false)
    {
        user.Username = user.Username.Trim();
        if (!update)
        {
            user.Establishments = new List<Establishment>();
            user.Roles = new List<Role>();
            user.Password = BCrypt.BCrypt.HashPassword(user.Password.Trim(), BCrypt.BCrypt.GenSalt());
            user.AccountNonExpired = true;
            user.AccountNonLocked = true;
            user.CredentialsNonExpired = true;
            user.Enabled = true;
        }
    }
}
