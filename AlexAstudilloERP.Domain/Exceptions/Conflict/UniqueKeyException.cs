using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Conflict;

public class UniqueKeyException : ConflictException
{
    public UniqueKeyException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
