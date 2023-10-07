using AlexAstudilloERP.Domain.Models.FirebaseAuth;
using FirebaseAdmin.Auth;

namespace AlexAstudilloERP.Domain.Interfaces.APIs;

public interface IFirebaseAuthAPI
{
    /// <summary>
    /// Change the user password.
    /// </summary>
    /// <param name="oobCode">The obb code to validate before change password.</param>
    /// <param name="password">The new user password.</param>
    /// <returns>A email of password changed.</returns>
    public Task<string> ConfirmPasswordReset(string oobCode, string password);

    public Task<UserRecord> CreateAsync(UserRecordArgs args);

    public Task<string> CreateCustomTokenAsync(string uid);

    public Task<DeleteUsersResult> DeleteAllAsync(List<string> uids);

    public Task DeleteAsync(string uid);

    public Task<FirebaseSignInResponse> ExchangeRefreshTokenForIdToken(string refreshToken);

    public Task<List<UserRecord>> GetAllAsync();

    public Task<UserRecord> GetByUidAsync(string uid);

    /// <summary>
    /// Send a email verification a specific user.
    /// </summary>
    /// <param name="idToken">The token of the user to send email.</param>
    /// <returns>A sent email.</returns>
    public Task<string> SendEmailVerification(string idToken);

    /// <summary>
    /// Send password reset to email provided.
    /// </summary>
    /// <param name="email">The email to send recovery link.</param>
    /// <returns>A sent email.</returns>
    public Task<string> SendPasswordResetEmail(string email);

    public Task<FirebaseSignInResponse> SignInWithEmail(string email, string password);

    public Task<FirebaseSignInResponse> SignUpWithEmailAsync(string email, string password);

    public Task<FirebaseToken> VerifyTokenAsync(string token);
}
