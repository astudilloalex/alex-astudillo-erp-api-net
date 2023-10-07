using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Models.FirebaseAuth;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IUserService
{
    public Task<User> ConfirmEmailVerificationAsync(string oobCode);

    public Task<string> ConfirmPasswordResetAsync(string oobCode, string newPassword);

    public Task<FirebaseSignInResponse> ExchangeRefreshTokenForIdToken(string refreshToken);

    public Task<User?> GetByCodeAsync(string code);

    public Task<string> SendEmailVerificationAsync(string idToken);

    public Task<string> SendPasswordResetEmailAsync(string email);

    public Task<FirebaseSignInResponse> SignIn(string email, string password);

    public Task<FirebaseSignInResponse> SignUp(string email, string password);
}
