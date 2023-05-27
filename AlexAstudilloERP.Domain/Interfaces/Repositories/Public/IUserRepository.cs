using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IUserRepository : INPRepository<User, long>
{
    public Task<bool> ExistsUsername(string username);

    public Task<User?> FindByUsernameOrEmail(string value);
}
