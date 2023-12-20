using AlexAstudilloERP.API.Attributes;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Exceptions.Firebase;
using AlexAstudilloERP.Domain.Exceptions.Unauthorized;
using AlexAstudilloERP.Domain.Interfaces.APIs;
using FirebaseAdmin.Auth;

namespace AlexAstudilloERP.API.Middlewares;

public class TokenValidationMiddleware(IFirebaseAuthAPI _authAPI, RequestDelegate _requestDelegate)
{
    public async Task Invoke(HttpContext context)
    {
        // If endpoint contains [SkipTokenValidation] do not validate token.
        if (context.GetEndpoint()?.Metadata.GetMetadata<SkipTokenValidationAttribute>() != null)
        {
            await _requestDelegate(context);
            return;
        }
        try
        {
            FirebaseToken token = await _authAPI.VerifyTokenAsync(context.Request.Headers.Authorization.ToString().Replace("Bearer ", ""));
            context.Request.Headers.Append("X-User-Code", token.Uid);
        }
        catch (FirebaseAuthException e)
        {
            throw new FirebaseException(e.AuthErrorCode?.ToString() ?? "expired-token");
        }
        catch
        {
            throw new UnauthorizedException(ExceptionEnum.InvalidToken);
        }
        await _requestDelegate(context);
    }
}
