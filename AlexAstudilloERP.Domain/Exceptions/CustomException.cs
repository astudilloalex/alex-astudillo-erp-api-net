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
                ExceptionEnum.AlreadyExistsCompanyWithThatIdCard => "already-exists-company-with-that-id-card",
                ExceptionEnum.CustomerAlreadyExists => "customer-already-exists",
                ExceptionEnum.EmailAlreadyInUse => "email-already-in-use",
                ExceptionEnum.EstablishmentAddressIsRequired => "establishment-address-is-required",
                ExceptionEnum.ExpiredAccount => "expired-account",
                ExceptionEnum.ExpiredActionCode => "expired-action-code",
                ExceptionEnum.ExpiredCredential => "expired-credential",
                ExceptionEnum.Forbidden => "forbidden",
                ExceptionEnum.IdCardAlreadyExists => "id-card-already-exists",
                ExceptionEnum.InvalidBirthdate => "invalid-birthdate",
                ExceptionEnum.InvalidCompanyName => "invalid-company-name",
                ExceptionEnum.InvalidCredential => "invalid-credential",
                ExceptionEnum.InvalidEmail => "invalid-email",
                ExceptionEnum.InvalidEstablishmentName => "invalid-establishment-name",
                ExceptionEnum.InvalidFirstName => "invalid-first-name",
                ExceptionEnum.InvalidIdCard => "invalid-id-card",
                ExceptionEnum.InvalidIdCardLength => "invalid-id-card-length",
                ExceptionEnum.InvalidLastName => "invalid-last-name",
                ExceptionEnum.InvalidMainStreet => "invalid-main-street",
                ExceptionEnum.InvalidPasswordLength => "invalid-password-length",
                ExceptionEnum.InvalidPersonNames => "invalid-person-names",
                ExceptionEnum.InvalidPoliticalDivisionType => "invalid-political-division-type",
                ExceptionEnum.InvalidProvinceCode => "invalid-province-code",
                ExceptionEnum.InvalidSocialReason => "invalid-social-reason",
                ExceptionEnum.InvalidToken => "invalid-token",
                ExceptionEnum.InvalidVerificationCode => "invalid-verification-code",
                ExceptionEnum.InvalidVerificationDigit => "invalid-verification-digit",
                ExceptionEnum.InvalidVerificationId => "invalid-verification-id",
                ExceptionEnum.LockedAccount => "locked-account",
                ExceptionEnum.MainEstablishmentAlreadyExists => "main-establishment-already-exists",
                ExceptionEnum.MainEstablishmentIsRequired => "main-establishment-is-required",
                ExceptionEnum.MembershipDoesNotAllowCreateCompany => "membership-does-not-allow-create-company",
                ExceptionEnum.OperationNotAllowed => "operation-not-allowed",
                ExceptionEnum.PermissionsAreRequired => "permissions-are-required",
                ExceptionEnum.RoleNameAlreadyExists => "role-name-already-exists",
                ExceptionEnum.TaxpayerNumberIsRequired => "taxpayer-number-is-required",
                ExceptionEnum.UserDisabled => "user-disabled",
                ExceptionEnum.UserIdCardAlreadyExists => "user-id-card-already-exists",
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
