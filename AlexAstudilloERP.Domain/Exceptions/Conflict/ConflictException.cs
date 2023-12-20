using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.Conflict;

public class ConflictException(ExceptionEnum exceptionEnum) : CustomException(exceptionEnum)
{
}
