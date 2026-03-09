namespace CumbrexSaaS.Domain.Enums;

/// <summary>Defines the channel through which a verification code is delivered.</summary>
public enum VerificationChannel
{
    /// <summary>Verification code sent via email.</summary>
    Email = 1,

    /// <summary>Verification code sent via SMS.</summary>
    Sms = 2,

    /// <summary>Verification code delivered via WhatsApp.</summary>
    WhatsApp = 3
}
