using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class EntityAlreadyExists : BadRequestException
{
    public EntityAlreadyExists(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
