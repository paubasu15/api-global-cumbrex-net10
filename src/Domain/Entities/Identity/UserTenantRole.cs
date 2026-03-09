using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents a role assignment for a user within a specific tenant.</summary>
public class UserTenantRole : BaseEntity
{
    /// <summary>Gets or sets the user tenant membership identifier.</summary>
    public Guid UserTenantId { get; set; }

    /// <summary>Gets or sets the role identifier.</summary>
    public Guid RoleId { get; set; }

    /// <summary>Gets or sets the user tenant navigation property.</summary>
    public UserTenant? UserTenant { get; set; }

    /// <summary>Gets or sets the role navigation property.</summary>
    public Role? Role { get; set; }
}
