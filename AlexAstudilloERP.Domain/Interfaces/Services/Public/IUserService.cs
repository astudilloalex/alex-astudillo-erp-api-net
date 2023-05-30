using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IUserService
{
    public Task<User?> GetByToken(string token);

    public Task<string> SignIn(string username, string password);

    public Task<User> SignUp(User user);
}
