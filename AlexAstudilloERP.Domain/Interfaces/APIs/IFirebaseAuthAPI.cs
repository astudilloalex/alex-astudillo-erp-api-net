using AlexAstudilloERP.Domain.Models.FirebaseAuth;
using FirebaseAdmin.Auth;

namespace AlexAstudilloERP.Domain.Interfaces.APIs;

public interface IFirebaseAuthAPI
{
    public Task<UserRecord> CreateAsync(UserRecordArgs args);

    public Task<string> CreateCustomTokenAsync(string uid);    

    public Task<DeleteUsersResult> DeleteAllAsync(List<string> uids);

    public Task DeleteAsync(string uid);

    public Task<List<UserRecord>> GetAllAsync();

    public Task<UserRecord> GetByUidAsync(string uid);

    public Task<FirebaseSignInResponse> SignInWithEmail(string email, string password);
}
