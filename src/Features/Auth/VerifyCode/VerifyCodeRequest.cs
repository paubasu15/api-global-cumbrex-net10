namespace CumbrexSaaS.Features.Auth.VerifyCode;

/// <summary>Request payload for verifying a one-time code.</summary>
public sealed record VerifyCodeRequest
{
    /// <summary>Gets or sets the email address the code was sent to.</summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>Gets or sets the 6-digit verification code.</summary>
    public string Code { get; init; } = string.Empty;
}
