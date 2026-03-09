using CumbrexSaaS.Domain.Common;
using CumbrexSaaS.Domain.Enums;

namespace CumbrexSaaS.Domain.Entities.Security;

/// <summary>Represents a one-time verification code sent to a user.</summary>
public class VerificationCode : BaseEntity
{
    /// <summary>Gets or sets the user identifier this code was issued to.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets or sets the hashed verification code.</summary>
    public string CodeHash { get; set; } = string.Empty;

    /// <summary>Gets or sets the channel through which the code was sent.</summary>
    public VerificationChannel Channel { get; set; }

    /// <summary>Gets or sets the destination (email or phone) the code was sent to.</summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>Gets or sets the expiry datetime of the code.</summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>Gets or sets a value indicating whether the code has been used.</summary>
    public bool IsUsed { get; set; }

    /// <summary>Gets or sets the datetime when the code was used.</summary>
    public DateTime? UsedAt { get; set; }

    /// <summary>Gets or sets the number of failed verification attempts.</summary>
    public int AttemptCount { get; set; }
}
