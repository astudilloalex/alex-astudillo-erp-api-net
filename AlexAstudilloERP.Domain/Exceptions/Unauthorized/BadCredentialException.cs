using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class BadCredentialException : UnauthorizedException
{
    public BadCredentialException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
