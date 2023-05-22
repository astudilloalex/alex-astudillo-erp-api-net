namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IUserService
{
    public Task<string> SignIn(string username, string password);
}
