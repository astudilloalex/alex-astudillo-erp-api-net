using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions;

public class CustomException : Exception
{
    private readonly ExceptionEnum _exceptionEnum;

    public CustomException(ExceptionEnum exceptionEnum, string message) : base(message)
    {
        _exceptionEnum = exceptionEnum;
    }

    public CustomException(ExceptionEnum exceptionEnum)
    {
        _exceptionEnum = exceptionEnum;
    }

    public string Code
    {
        get
        {
            return _exceptionEnum switch
            {
                ExceptionEnum.WrongPassword => "wrong-password",
                _ => _exceptionEnum.ToString(),
            };
        }
    }
}
