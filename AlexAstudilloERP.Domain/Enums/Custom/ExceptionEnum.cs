namespace AlexAstudilloERP.Domain.Enums.Custom;

public enum ExceptionEnum : short
{
    /// <summary>
    /// Thrown if there already exists an account with the email address asserted by the credential.
    /// </summary>
    AccountExistsWithDifferentCredential,
    /// <summary>
    /// Thrown if there already exists an account with the given email address.
    /// </summary>
    EmailAlreadyInUse,
    /// <summary>
    /// Thrown if the credential is malformed or has expired.
    /// </summary>
    InvalidCredential,
    /// <summary>
    /// Thrown if the email address is not valid.
    /// </summary>
    InvalidEmail,
    /// <summary>
    /// If operation is not allowed.
    /// </summary>
    OperationNotAllowed,
    /// <summary>
    /// Thrown if the user corresponding to the given email has been disabled.
    /// </summary>
    UserDisabled,
    /// <summary>
    /// Thrown if there is no user corresponding to the given email or username.
    /// </summary>
    UserNotFound,
    /// <summary>
    /// Thrown if the password is not strong enough.
    /// </summary>
    WeakPassword,
    /// <summary>
    /// Thrown if the password is invalid for the given email, or the account corresponding to the email doesn't have a password set.
    /// </summary>
    WrongPassword,
}
