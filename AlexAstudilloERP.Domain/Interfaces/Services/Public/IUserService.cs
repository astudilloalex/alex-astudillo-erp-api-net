using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Models.FirebaseAuth;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IUserService
{
    public Task<User?> GetByCodeAsync(string code);

    public Task<FirebaseSignInResponse> SignIn(string email, string password);

    public Task<User> SignUp(string email, string password);
}
