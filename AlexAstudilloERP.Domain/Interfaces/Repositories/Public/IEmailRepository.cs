using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IEmailRepository : INPRepository<Email, int>
{
    public Task<bool> ExistsMail(string mail);

    public Task<Email?> FindByMail(string mail);
}
