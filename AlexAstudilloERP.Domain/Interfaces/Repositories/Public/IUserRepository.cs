using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IUserRepository
{
    public Task<User> ChangePasswordAsync(User entity, bool multithread = false);

    public Task<bool> ExistsByEmail(string mail);

    public Task<bool> ExistsByIdCard(string idCard);

    public Task<bool> ExistsUsername(string username);

    public Task<User?> FindByCodeAsync(string code, bool multithread = false);

    public Task<User?> FindByIdCard(string idCard);

    public Task<User?> FindByEmailAsync(string email);

    public Task<User> SaveAsync(User entity, bool multithread = false);

    public Task<User> VerifyEmailAsync(User user, bool multithread = false);
}
