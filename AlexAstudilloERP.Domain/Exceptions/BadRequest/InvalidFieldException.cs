using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class InvalidFieldException : BadRequestException
{
    public InvalidFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
