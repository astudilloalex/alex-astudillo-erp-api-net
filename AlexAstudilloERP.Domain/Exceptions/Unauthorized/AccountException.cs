using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class AccountException(ExceptionEnum exceptionEnum) : UnauthorizedException(exceptionEnum)
{
}
