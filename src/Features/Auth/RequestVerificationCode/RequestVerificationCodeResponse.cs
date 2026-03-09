namespace CumbrexSaaS.Features.Auth.RequestVerificationCode;

/// <summary>Response returned after requesting a verification code.</summary>
public sealed record RequestVerificationCodeResponse
{
    /// <summary>Gets or sets a value indicating whether the request was accepted.</summary>
    public bool Success { get; init; }

    /// <summary>Gets or sets the message to display to the user.</summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>Gets or sets the number of seconds until the code expires.</summary>
    public int ExpiresInSeconds { get; init; }
}
