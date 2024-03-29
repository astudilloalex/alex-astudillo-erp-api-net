﻿namespace AlexAstudilloERP.Domain.Exceptions.Firebase;

public class FirebaseException(string _message) : Exception
{
    public string Code
    {
        get
        {

            return _message switch
            {
                "EMAIL_EXISTS" => "email-already-in-use",
                "ExpiredIdToken" => "expired-token",
                "EXPIRED_OOB_CODE" => "expired-oob-code",
                "INVALID_OOB_CODE" => "invalid-oob-code",
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
