using CumbrexSaaS.Domain.Common;
using CumbrexSaaS.Domain.Enums;

namespace CumbrexSaaS.Domain.Entities.Security;

/// <summary>Represents a recorded login attempt for security auditing.</summary>
public class LoginAttempt : BaseEntity
{
    /// <summary>Gets or sets the email address used in the login attempt.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Gets or sets the IP address of the client.</summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>Gets or sets the user agent string.</summary>
    public string? UserAgent { get; set; }

    /// <summary>Gets or sets a value indicating whether the attempt was successful.</summary>
    public bool IsSuccess { get; set; }

    /// <summary>Gets or sets the failure reason if the attempt was unsuccessful.</summary>
    public LoginFailureReason? FailureReason { get; set; }

    /// <summary>Gets or sets the user identifier if the user was found.</summary>
    public Guid? UserId { get; set; }

    /// <summary>Gets or sets the tenant identifier associated with this attempt.</summary>
    public Guid? TenantId { get; set; }
}
