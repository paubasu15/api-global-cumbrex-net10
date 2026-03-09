using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents a system-level permission that can be assigned to roles.</summary>
public class Permission : BaseEntity
{
    /// <summary>Gets or sets the unique permission code.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the permission.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the permission group (for organization in the UI).</summary>
    public string? Group { get; set; }

    /// <summary>Gets or sets the role permissions that include this permission.</summary>
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
