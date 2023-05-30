using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.Conflict;
using AlexAstudilloERP.Domain.Exceptions.Unauthorized;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class UserService : IUserService
{
    private readonly IEmailRepository _emailRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUserRepository _repository;
    private readonly ISetData _setData;
    private readonly ITokenService _tokenService;
    private readonly IValidateData _validateData;

    public UserService(IEmailRepository emailRepository, IPersonRepository personRepository, IUserRepository repository, ISetData setData, ITokenService tokenService, IValidateData validateData)
    {
        _emailRepository = emailRepository;
        _personRepository = personRepository;
        _repository = repository;
        _setData = setData;
        _tokenService = tokenService;
        _validateData = validateData;
    }

    public async Task<User?> GetByToken(string token)
    {
        return await _repository.FindByIdAsync(_tokenService.GetUserId(token));
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
        _setData.SetUserData(user, update: false);
        _validateData.ValidatePassword(user.Password);
        await _validateData.ValidateUser(user);
        // Validate person.
        if (user.Person != null)
        {
            _setData.SetPersonData(user.Person);
            if (await _repository.ExistsByIdCard(user.Person.IdCard)) throw new ConflictException(ExceptionEnum.UserIdCardAlreadyExists);
            Person? person = await _personRepository.FindByIdCard(user.Person.IdCard);
            if (person == null)
            {
                await _validateData.ValidatePerson(user.Person, update: false);
            }
            else
            {
                user.Person = null;
                user.PersonId = person.Id;
            }
        }
        // Validate email.
        if (user.Email != null)
        {
            _setData.SetEmailData(email: user.Email, update: false);
            if (await _repository.ExistsByEmail(user.Email.Mail)) throw new ConflictException(ExceptionEnum.EmailAlreadyInUse);
            Email? email = await _emailRepository.FindByMail(user.Email.Mail);
            if (email == null)
            {
                _validateData.ValidateMail(user.Email.Mail);
            }
            else
            {
                user.Email = null;
                user.EmailId = email.Id;
            }
        }
        return await _repository.SaveAsync(user);
    }
}
