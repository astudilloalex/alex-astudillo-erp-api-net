using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class BadCredentialException(ExceptionEnum exceptionEnum) : UnauthorizedException(exceptionEnum)
{
}
