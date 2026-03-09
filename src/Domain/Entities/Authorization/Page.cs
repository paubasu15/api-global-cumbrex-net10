using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Authorization;

/// <summary>Represents a navigable page or module in the application UI.</summary>
public class Page : BaseEntity
{
    /// <summary>Gets or sets the unique code for the page.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the page.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the optional parent page identifier for hierarchical navigation.</summary>
    public Guid? ParentId { get; set; }

    /// <summary>Gets or sets the display order.</summary>
    public int Order { get; set; }

    /// <summary>Gets or sets the icon identifier for UI rendering.</summary>
    public string? Icon { get; set; }

    /// <summary>Gets or sets the route path for this page.</summary>
    public string? Route { get; set; }

    /// <summary>Gets or sets the actions available on this page.</summary>
    public ICollection<PageAction> Actions { get; set; } = new List<PageAction>();

    /// <summary>Gets or sets the role page permissions for this page.</summary>
    public ICollection<RolePagePermission> RolePagePermissions { get; set; } = new List<RolePagePermission>();
}
