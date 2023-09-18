namespace AlexAstudilloERP.Domain.Exceptions.Firebase;

public class FirebaseException : Exception
{
    private readonly string _message;

    public FirebaseException(string message)
    {
        _message = message;
    }

    public string Code
    {
        get
        {

            return _message switch
            {
                "EMAIL_EXISTS" => "email-already-in-use",
                "EXPIRED_OOB_CODE" => "expired-obb-code",
                "INVALID_OOB_CODE" => "invalid-obb-code",
                "OPERATION_NOT_ALLOWED" => "operation-not-allowed",
                "TOO_MANY_ATTEMPTS_TRY_LATER" => "too-many-attemps-try-later",
                "EMAIL_NOT_FOUND" => "email-not-found",
                "INVALID_PASSWORD" => "wrong-password",
                "USER_DISABLED" => "user-disabled",
                "INVALID_REFRESH_TOKEN" => "invalid-refresh-token",
                "MISSING_REFRESH_TOKEN" => "missing-refresh-token",
                "TOKEN_EXPIRED" => "expired-token",
                _ => _message,
            };
        }
    }
}
