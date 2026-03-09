using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Security;

/// <summary>Represents a JWT refresh token issued to a user session.</summary>
public class RefreshToken : BaseEntity
{
    /// <summary>Gets or sets the user session identifier.</summary>
    public Guid UserSessionId { get; set; }

    /// <summary>Gets or sets the hashed refresh token value.</summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>Gets or sets the expiry datetime.</summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>Gets or sets a value indicating whether the token has been revoked.</summary>
    public bool IsRevoked { get; set; }

    /// <summary>Gets or sets the datetime when the token was revoked.</summary>
    public DateTime? RevokedAt { get; set; }

    /// <summary>Gets or sets the user session navigation property.</summary>
    public UserSession? UserSession { get; set; }
}
