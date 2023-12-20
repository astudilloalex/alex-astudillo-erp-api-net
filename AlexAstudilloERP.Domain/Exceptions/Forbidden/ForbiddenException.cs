using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Forbidden;

public class ForbiddenException(ExceptionEnum exceptionEnum) : CustomException(exceptionEnum)
{
}
