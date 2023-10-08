using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Interfaces.APIs;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using AlexAstudilloERP.Domain.Models.FirebaseAuth;
using FirebaseAdmin.Auth;

namespace AlexAstudilloERP.Application.Services.Public;

public class UserService : IUserService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUserRepository _repository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;
    private readonly IFirebaseAuthAPI _firebaseAuthAPI;
    private readonly IUtil _util;

    public UserService(IEmailRepository emailRepository, IPersonRepository personRepository, IUserRepository repository, ISetData setData, IValidateData validateData, IFirebaseAuthAPI firebaseAuthAPI, IUtil util)
    {
        _emailRepository = emailRepository;
        _personRepository = personRepository;
        _repository = repository;
        _setData = setData;
        _validateData = validateData;
        _firebaseAuthAPI = firebaseAuthAPI;
        _util = util;
    }

    public async Task<User> ConfirmEmailVerificationAsync(string oobCode)
    {
        string userCode = await _firebaseAuthAPI.ConfirmEmailVerification(oobCode);
        return await _repository.VerifyEmailAsync(new()
        {
            Code = userCode,
        });
    }

    public async Task<string> ConfirmPasswordResetAsync(string oobCode, string newPassword)
    {
        string email = await _firebaseAuthAPI.ConfirmPasswordReset(oobCode, newPassword);
        _ = Task.Run(() => _repository.ChangePasswordAsync(new()
        {
            Email = email,
            Password = BCrypt.BCrypt.HashPassword(newPassword, BCrypt.BCrypt.GenSalt(12))
        }, multithread: true)).ConfigureAwait(false);
        return email;
    }

    public Task<FirebaseSignInResponse> ExchangeRefreshTokenForIdToken(string refreshToken)
    {
        return _firebaseAuthAPI.ExchangeRefreshTokenForIdToken(refreshToken);
    }

    public Task<User?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<string> SendEmailVerificationAsync(string idToken)
    {
        return _firebaseAuthAPI.SendEmailVerification(idToken);
    }

    public Task<string> SendPasswordResetEmailAsync(string email)
    {
        return _firebaseAuthAPI.SendPasswordResetEmail(email);
    }

    public Task<FirebaseSignInResponse> SignIn(string email, string password)
    {
        return _firebaseAuthAPI.SignInWithEmail(email, password);
    }

    public async Task<FirebaseSignInResponse> SignUp(string email, string password)
    {
        _validateData.ValidateMail(email);
        _validateData.ValidatePassword(password);
        FirebaseSignInResponse firebaseResponse = await _firebaseAuthAPI.SignUpWithEmailAsync(email, password);
        try
        {
            UserRecord userRecord = await _firebaseAuthAPI.GetByUidAsync(firebaseResponse.LocalId);
            User saved = await _repository.SaveAsync(new()
            {
                AuthProviders = new List<AuthProvider>
                {
                    new()
                    {
                        Id = (short)AuthProviderEnum.Password,
                    }
                },
                Code = firebaseResponse.LocalId,
                EmailVerified = false,
                Email = email,
                Password = BCrypt.BCrypt.HashPassword(password, BCrypt.BCrypt.GenSalt(12)),
                UserMetadatum = new()
                {
                    CreationDate = userRecord.UserMetaData.CreationTimestamp ?? DateTime.Now,
                    LastRefreshDate = userRecord.UserMetaData?.LastRefreshTimestamp,
                    LastSignInDate = userRecord.UserMetaData?.LastSignInTimestamp,
                }
            }, multithread: true);
        }
        catch
        {
            await _firebaseAuthAPI.DeleteAsync(firebaseResponse.LocalId);
            throw;
        }
        return firebaseResponse;
    }
}
