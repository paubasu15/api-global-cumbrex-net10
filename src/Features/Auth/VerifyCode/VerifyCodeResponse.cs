namespace CumbrexSaaS.Features.Auth.VerifyCode;

/// <summary>Response returned after successfully verifying a code.</summary>
public sealed record VerifyCodeResponse
{
    /// <summary>Gets or sets a value indicating whether authentication succeeded.</summary>
    public bool Success { get; init; }

    /// <summary>Gets or sets the JWT access token (also set as an HTTP-only cookie).</summary>
    public string? AccessToken { get; init; }

    /// <summary>Gets or sets the user's identifier.</summary>
    public Guid? UserId { get; init; }

    /// <summary>Gets or sets the user's email address.</summary>
    public string? Email { get; init; }

    /// <summary>Gets or sets an error message if authentication failed.</summary>
    public string? ErrorMessage { get; init; }
}
