using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Authorization;

/// <summary>Represents an action that can be performed on a page (e.g., View, Create, Edit, Delete).</summary>
public class PageAction : BaseEntity
{
    /// <summary>Gets or sets the page identifier.</summary>
    public Guid PageId { get; set; }

    /// <summary>Gets or sets the action code (e.g., VIEW, CREATE, EDIT, DELETE).</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the action.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the page navigation property.</summary>
    public Page? Page { get; set; }

    /// <summary>Gets or sets the role page permissions for this action.</summary>
    public ICollection<RolePagePermission> RolePagePermissions { get; set; } = new List<RolePagePermission>();
}
