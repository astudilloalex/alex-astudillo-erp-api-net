using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Conflict;

public class ConflictException : CustomException
{
    public ConflictException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
