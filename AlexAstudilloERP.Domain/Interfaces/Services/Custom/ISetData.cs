using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface ISetData
{
    public void SetPersonData(Person person)
    {
        person.IdCard = person.IdCard.Trim().ToUpper();
        person.FirstName = person.JuridicalPerson ? null : person.FirstName?.Trim().ToUpperInvariant();
        person.LastName = person.JuridicalPerson ? null : person.LastName?.Trim().ToUpperInvariant();
        person.SocialReason = person.JuridicalPerson ? person.SocialReason?.Trim().ToUpperInvariant() : null;
    }

    public void SetUserData(User user, bool update = false)
    {
        user.Username = user.Username.Trim();
        user.Password = user.Password.Trim();
        if (!update)
        {
            user.AccountNonExpired = true;
            user.AccountNonLocked = true;
            user.CredentialsNonExpired = true;
            user.Enabled = true;
        }
    }
}
