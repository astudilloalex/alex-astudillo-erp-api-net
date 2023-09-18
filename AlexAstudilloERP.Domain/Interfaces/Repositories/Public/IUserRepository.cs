using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IUserRepository
{
    public Task<bool> ExistsByEmail(string mail);

    public Task<bool> ExistsByIdCard(string idCard);

    public Task<bool> ExistsUsername(string username);

    public Task<User?> FindByIdCard(string idCard);

    public Task<User?> FindByEmailAsync(string email);

    public Task<User> SaveAsync(User entity, bool multithread = false);
}
