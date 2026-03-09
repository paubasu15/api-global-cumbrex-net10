using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents a user identity in the platform.</summary>
public class User : BaseEntity
{
    /// <summary>Gets or sets the user's email address (globally unique).</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Gets or sets the user's first name.</summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>Gets or sets the user's last name.</summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>Gets or sets an optional phone number.</summary>
    public string? Phone { get; set; }

    /// <summary>Gets or sets the URL of the user's avatar image.</summary>
    public string? AvatarUrl { get; set; }

    /// <summary>Gets or sets a value indicating whether the user account is globally active.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the datetime when the email was verified.</summary>
    public DateTime? EmailVerifiedAt { get; set; }

    /// <summary>Gets or sets the tenant memberships for this user.</summary>
    public ICollection<UserTenant> UserTenants { get; set; } = new List<UserTenant>();
}
