using CumbrexSaaS.Domain.Common;
using CumbrexSaaS.Domain.Enums;

namespace CumbrexSaaS.Domain.Entities.Security;

/// <summary>Represents an active authenticated user session.</summary>
public class UserSession : BaseEntity
{
    /// <summary>Gets or sets the user identifier.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets or sets the tenant identifier for this session.</summary>
    public Guid TenantId { get; set; }

    /// <summary>Gets or sets the IP address of the client.</summary>
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>Gets or sets the user agent string.</summary>
    public string? UserAgent { get; set; }

    /// <summary>Gets or sets the device type.</summary>
    public DeviceType DeviceType { get; set; }

    /// <summary>Gets or sets the datetime when the session was last active.</summary>
    public DateTime LastActiveAt { get; set; }

    /// <summary>Gets or sets the datetime when the session expires.</summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>Gets or sets a value indicating whether the session is active.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the refresh tokens for this session.</summary>
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
