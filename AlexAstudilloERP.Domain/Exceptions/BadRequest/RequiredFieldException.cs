using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class RequiredFieldException(ExceptionEnum exceptionEnum) : BadRequestException(exceptionEnum)
{
}
