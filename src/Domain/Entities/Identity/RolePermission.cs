using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Identity;

/// <summary>Represents the many-to-many relationship between roles and permissions.</summary>
public class RolePermission : BaseEntity
{
    /// <summary>Gets or sets the role identifier.</summary>
    public Guid RoleId { get; set; }

    /// <summary>Gets or sets the permission identifier.</summary>
    public Guid PermissionId { get; set; }

    /// <summary>Gets or sets the role navigation property.</summary>
    public Role? Role { get; set; }

    /// <summary>Gets or sets the permission navigation property.</summary>
    public Permission? Permission { get; set; }
}
