using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class AccountException : UnauthorizedException
{
    public AccountException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
