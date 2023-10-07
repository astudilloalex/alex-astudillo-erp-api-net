using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Models.FirebaseAuth;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IUserService
{
    public Task<FirebaseSignInResponse> ExchangeRefreshTokenForIdToken(string refreshToken);

    public Task<User?> GetByCodeAsync(string code);

    public Task<string> SendPasswordResetEmailAsync(string email);

    public Task<FirebaseSignInResponse> SignIn(string email, string password);

    public Task<FirebaseSignInResponse> SignUp(string email, string password);
}
