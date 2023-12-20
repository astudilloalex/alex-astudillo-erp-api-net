using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class InvalidFieldException(ExceptionEnum exceptionEnum) : BadRequestException(exceptionEnum)
{
}
