using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface IValidateData
{
    public void ValidateCompany(Company company, bool update = false);
    public void ValidateEmail(Email email, bool update = false);
    public void ValidatePerson(Person person, bool update = false);
}
