using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Forbidden;

public class ForbiddenException : CustomException
{
    public ForbiddenException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
