using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents the membership link between a user and a tenant.</summary>
public class UserTenant : BaseEntity
{
    /// <summary>Gets or sets the user identifier.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets or sets the tenant identifier.</summary>
    public Guid TenantId { get; set; }

    /// <summary>Gets or sets a value indicating whether the user is active within this tenant.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the user navigation property.</summary>
    public User? User { get; set; }

    /// <summary>Gets or sets the roles assigned to the user within this tenant.</summary>
    public ICollection<UserTenantRole> UserTenantRoles { get; set; } = new List<UserTenantRole>();
}
