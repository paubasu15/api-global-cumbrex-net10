namespace CumbrexSaaS.Domain.Enums;

/// <summary>Represents the reason a login attempt failed.</summary>
public enum LoginFailureReason
{
    /// <summary>User account was not found.</summary>
    UserNotFound = 1,

    /// <summary>Invalid verification code provided.</summary>
    InvalidCode = 2,

    /// <summary>Verification code has expired.</summary>
    CodeExpired = 3,

    /// <summary>Account is suspended or deactivated.</summary>
    AccountInactive = 4,

    /// <summary>Tenant account is inactive or suspended.</summary>
    TenantInactive = 5,

    /// <summary>Too many failed attempts; account temporarily locked.</summary>
    TooManyAttempts = 6
}
