using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Unauthorized;
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
    private readonly ITokenService _tokenService;
    private readonly IValidateData _validateData;
    private readonly IFirebaseAuthAPI _firebaseAuthAPI;
    private readonly IUtil _util;

    public UserService(IEmailRepository emailRepository, IPersonRepository personRepository, IUserRepository repository, ISetData setData, ITokenService tokenService, IValidateData validateData, IFirebaseAuthAPI firebaseAuthAPI, IUtil util)
    {
        _emailRepository = emailRepository;
        _personRepository = personRepository;
        _repository = repository;
        _setData = setData;
        _tokenService = tokenService;
        _validateData = validateData;
        _firebaseAuthAPI = firebaseAuthAPI;
        _util = util;
    }

    public Task<User?> GetByToken(string token)
    {
        throw new NotImplementedException();
        //return await _repository.FindByIdAsync(_tokenService.GetUserId(token));
    }

    public Task<FirebaseSignInResponse> SignIn(string email, string password)
    {
        return _firebaseAuthAPI.SignInWithEmail(email, password);
    }

    public async Task<User> SignUp(string email, string password)
    {
        _validateData.ValidateMail(email);
        _validateData.ValidatePassword(password);
        // Verify if exists email.
        if (await _repository.ExistsByEmail(email)) throw new UniqueKeyException(ExceptionEnum.EmailAlreadyInUse);
        // Save the user on Firebase
        UserRecord userRecord = await _firebaseAuthAPI.CreateAsync(new()
        {
            Disabled = false,
            Email = email,
            EmailVerified = false,
            Password = password,
        });
        // Set data to the user.
        User saved = new()
        {
            AuthProviders = new List<AuthProvider>
            {
                new()
                {
                    Id = (short)AuthProviderEnum.Password,
                }
            },
            Code = userRecord.Uid,
            EmailVerified = false,
            Email = email,
            Password = BCrypt.BCrypt.HashPassword(password, BCrypt.BCrypt.GenSalt()),
        };
        try
        {
            saved = await _repository.SaveAsync(saved);
        }
        catch
        {
            await _firebaseAuthAPI.DeleteAsync(userRecord.Uid);
            throw;
        }
        return saved;
    }
}
