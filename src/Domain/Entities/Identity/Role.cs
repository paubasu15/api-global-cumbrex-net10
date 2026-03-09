using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents a role that can be assigned to users within a tenant.</summary>
public class Role : BaseTenantEntity
{
    /// <summary>Gets or sets the role code (unique within a tenant).</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the role.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the role description.</summary>
    public string? Description { get; set; }

    /// <summary>Gets or sets a value indicating whether this is a system-defined role.</summary>
    public bool IsSystem { get; set; }

    /// <summary>Gets or sets a value indicating whether the role is active.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the permissions assigned to this role.</summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    /// <summary>Gets or sets the user tenant role assignments for this role.</summary>
    public ICollection<UserTenantRole> UserTenantRoles { get; set; } = new List<UserTenantRole>();
}
