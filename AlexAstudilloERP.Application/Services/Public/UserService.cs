using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.Unauthorized;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ISetData _setData;
    private readonly ITokenService _tokenService;
    private readonly IValidateData _validateData;

    public UserService(IUserRepository repository, ISetData setData, ITokenService tokenService, IValidateData validateData)
    {
        _repository = repository;
        _setData = setData;
        _tokenService = tokenService;
        _validateData = validateData;
    }

    public async Task<string> SignIn(string username, string password)
    {
        User? user = await _repository.FindByUsernameOrEmail(username.Trim()) ?? throw new BadCredentialException(ExceptionEnum.UserNotFound);
        if (!BCrypt.BCrypt.CheckPassword(password, user.Password)) throw new BadCredentialException(ExceptionEnum.WrongPassword);
        if (!user.AccountNonExpired) throw new AccountException(ExceptionEnum.ExpiredAccount);
        if (!user.AccountNonLocked) throw new AccountException(ExceptionEnum.LockedAccount);
        if (!user.CredentialsNonExpired) throw new AccountException(ExceptionEnum.ExpiredCredential);
        if (!user.Enabled) throw new AccountException(ExceptionEnum.UserDisabled);
        return _tokenService.GenerateToken(user);
    }

    public async Task<User> SignUp(User user)
    {
        await _validateData.ValidateUser(user);
        throw new NotImplementedException();
    }
}
