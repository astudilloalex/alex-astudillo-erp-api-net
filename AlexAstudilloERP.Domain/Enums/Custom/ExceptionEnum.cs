﻿namespace AlexAstudilloERP.Domain.Enums.Custom;

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
    /// Thrown if the establishment does not contains an address.
    /// </summary>
    EstablishmentAddressIsRequired,
    /// <summary>
    /// Thrown if account expired.
    /// </summary>
    ExpiredAccount,
    /// <summary>
    /// Thrown if OTP in email link expires.
    /// </summary>
    ExpiredActionCode,
    /// <summary>
    /// Thrown if credentials expired.
    /// </summary>
    ExpiredCredential,
    /// <summary>
    /// Thrown if the id card already exists.
    /// </summary>
    IdCardAlreadyExists,
    /// <summary>
    /// Thrown if the company name is invalid.
    /// </summary>
    InvalidCompanyName,
    /// <summary>
    /// Thrown if the credential is malformed or has expired.
    /// </summary>
    InvalidCredential,
    /// <summary>
    /// Thrown if the email address is not valid.
    /// </summary>
    InvalidEmail,
    /// <summary>
    /// Thrown if the establishment name is invalid.
    /// </summary>
    InvalidEstablishmentName,
    /// <summary>
    /// Thrown if person first name is invalid.
    /// </summary>
    InvalidFirstName,
    /// <summary>
    /// Thrown if person last name is invalid.
    /// </summary>
    InvalidLastName,
    /// <summary>
    /// Thrown if address main street is invalid.
    /// </summary>
    InvalidMainStreet,
    /// <summary>
    /// Thrown if the password length is invalid.
    /// </summary>
    InvalidPasswordLength,
    /// <summary>
    /// Thrown if the person first name or last name or social reason are invalid.
    /// </summary>
    InvalidPersonNames,
    /// <summary>
    /// Thrown if the person social reason is invalid.
    /// </summary>
    InvalidSocialReason,
    /// <summary>
    /// Thrown if verification code of the credential is not valid.
    /// </summary>
    InvalidVerificationCode,
    /// <summary>
    /// Thrown if verification ID of the credential is not valid.
    /// </summary>
    InvalidVerificationId,
    /// <summary>
    /// Thrown if locked account.
    /// </summary>
    LockedAccount,
    /// <summary>
    /// Thrown if company does not contains a main establishment.
    /// </summary>
    MainEstablishmentIsRequired,
    /// <summary>
    /// If operation is not allowed.
    /// </summary>
    OperationNotAllowed,
    /// <summary>
    /// Thrown if the user corresponding to the given email has been disabled.
    /// </summary>
    UserDisabled,
    /// <summary>
    /// Thrown if exists a user with id card.
    /// </summary>
    UserIdCardAlreadyExists,
    /// <summary>
    /// Thrown if the credential given does not correspond to the user.
    /// </summary>
    UserMismatch,
    /// <summary>
    /// Thrown if username already exists.
    /// </summary>
    UsernameAlreadyExist,
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
