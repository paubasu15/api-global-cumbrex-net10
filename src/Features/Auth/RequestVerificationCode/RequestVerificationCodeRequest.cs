namespace CumbrexSaaS.Features.Auth.RequestVerificationCode;

/// <summary>Request payload for requesting a verification code.</summary>
public sealed record RequestVerificationCodeRequest
{
    /// <summary>Gets or sets the email address to send the verification code to.</summary>
    public string Email { get; init; } = string.Empty;
}
