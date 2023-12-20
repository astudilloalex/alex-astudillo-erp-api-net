using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class BadRequestException(ExceptionEnum exceptionEnum) : CustomException(exceptionEnum)
{
}
