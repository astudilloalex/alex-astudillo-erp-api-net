using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> SignIn(string username, string password)
    {
        User? user = await _repository.FindByUsernameOrEmail(username.Trim());
        BCrypt.BCrypt.CheckPassword(username, password);
        throw new NotImplementedException();
    }
}
