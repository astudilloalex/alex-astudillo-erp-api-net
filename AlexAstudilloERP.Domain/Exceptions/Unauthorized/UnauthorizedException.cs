using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
