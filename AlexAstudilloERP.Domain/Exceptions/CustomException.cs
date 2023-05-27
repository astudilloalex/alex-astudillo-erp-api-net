using AlexAstudilloERP.Domain.Enums.Custom;

namespace AlexAstudilloERP.Domain.Exceptions;

public class CustomException : Exception
{
    private readonly ExceptionEnum _exceptionEnum;

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
                ExceptionEnum.AccountExistsWithDifferentCredential => "account-exists-with-different-credential",
                ExceptionEnum.EmailAlreadyInUse => "email-already-in-use",
                ExceptionEnum.ExpiredAccount => "expired-account",
                ExceptionEnum.ExpiredActionCode => "expired-action-code",
                ExceptionEnum.ExpiredCredential => "expired-credential",
                ExceptionEnum.IdCardAlreadyExists => "id-card-already-exists",
                ExceptionEnum.InvalidCredential => "invalid-credential",
                ExceptionEnum.InvalidEmail => "invalid-email",
                ExceptionEnum.InvalidFirstName => "invalid-first-name",
                ExceptionEnum.InvalidLastName => "invalid-last-name",
                ExceptionEnum.InvalidPasswordLength => "invalid-password-length",
                ExceptionEnum.InvalidPersonNames => "invalid-person-names",
                ExceptionEnum.InvalidSocialReason => "invalid-social-reason",
                ExceptionEnum.InvalidVerificationCode => "invalid-verification-code",
                ExceptionEnum.InvalidVerificationId => "invalid-verification-id",
                ExceptionEnum.LockedAccount => "locked-account",
                ExceptionEnum.OperationNotAllowed => "operation-not-allowed",
                ExceptionEnum.UserDisabled => "user-disabled",
                ExceptionEnum.UserMismatch => "user-mismatch",
                ExceptionEnum.UsernameAlreadyExist => "username-already-exist",
                ExceptionEnum.UserNotFound => "user-not-found",
                ExceptionEnum.WeakPassword => "weak-password",
                ExceptionEnum.WrongPassword => "wrong-password",
                _ => _exceptionEnum.ToString(),
            };
        }
    }
}
