using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IUserRepository : INPRepository<User, long>
{
    public Task<bool> ExistsByEmail(string mail);

    public Task<bool> ExistsByIdCard(string idCard);

    public Task<bool> ExistsUsername(string username);

    public Task<User?> FindByIdCard(string idCard);

    public Task<User?> FindByUsernameOrEmail(string value);
}
