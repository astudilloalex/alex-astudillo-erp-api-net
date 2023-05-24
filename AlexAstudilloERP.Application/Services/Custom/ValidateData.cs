using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;

namespace AlexAstudilloERP.Application.Services.Custom
{
    public class ValidateData : IValidateData
    {
        public void ValidateCompany(Company company, bool update = false)
        {
            throw new NotImplementedException();
        }

        public void ValidateEmail(Email email, bool update = false)
        {
            throw new NotImplementedException();
        }

        public void ValidatePerson(Person person, bool update = false)
        {
            throw new NotImplementedException();
        }
    }
}
