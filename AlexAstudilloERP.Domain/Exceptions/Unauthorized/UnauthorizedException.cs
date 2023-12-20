using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Unauthorized;

public class UnauthorizedException(ExceptionEnum exceptionEnum) : CustomException(exceptionEnum)
{
}
