using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Conflict;

public class UniqueKeyException(ExceptionEnum exceptionEnum) : ConflictException(exceptionEnum)
{
}
