using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions.BadRequest;

public class BadRequestException : CustomException
{
    public BadRequestException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
