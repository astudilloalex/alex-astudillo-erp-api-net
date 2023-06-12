using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface IValidateData
{
    public Task ValidateAddress(Address address, bool update = false);
    public Task ValidateCompany(Company company, bool update = false);
    public Task ValidateEmail(Email email, bool update = false);
    public Task ValidateEstablishment(Establishment establishment, bool update = false);
    public void ValidateMail(string mail);
    public void ValidatePassword(string password);
    public Task ValidatePerson(Person person, bool update = false);
    public Task ValidateUser(User user, bool update = false);
}
