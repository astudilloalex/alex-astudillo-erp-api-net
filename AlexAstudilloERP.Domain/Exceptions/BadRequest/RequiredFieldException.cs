using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class RequiredFieldException : BadRequestException
{
    public RequiredFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
