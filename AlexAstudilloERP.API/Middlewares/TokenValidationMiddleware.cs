using AlexAstudilloERP.Domain.Interfaces.APIs;

namespace AlexAstudilloERP.API.Middlewares;

public class TokenValidationMiddleware
{
    private readonly IFirebaseAuthAPI _authAPI;

    public TokenValidationMiddleware(IFirebaseAuthAPI authAPI)
    {
        _authAPI = authAPI;
    }

    public async Task Invoke(HttpContext context)
    {

    }
}
