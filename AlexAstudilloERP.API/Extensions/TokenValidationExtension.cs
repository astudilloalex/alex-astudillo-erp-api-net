using AlexAstudilloERP.API.Middlewares;

namespace AlexAstudilloERP.API.Extensions;

public static class TokenValidationExtension
{
    public static IServiceCollection AddTokenValidationMiddleware(this IServiceCollection services)
    {
        return services.AddTransient<TokenValidationMiddleware>();
    }
}
