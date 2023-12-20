using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class EntityAlreadyExists(ExceptionEnum exceptionEnum) : BadRequestException(exceptionEnum)
{
}
