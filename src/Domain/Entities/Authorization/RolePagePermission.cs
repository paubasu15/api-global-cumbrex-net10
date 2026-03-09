using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Authorization;

/// <summary>Represents the permission granted to a role for a specific page action.</summary>
public class RolePagePermission : BaseTenantEntity
{
    /// <summary>Gets or sets the role identifier.</summary>
    public Guid RoleId { get; set; }

    /// <summary>Gets or sets the page identifier.</summary>
    public Guid PageId { get; set; }

    /// <summary>Gets or sets the page action identifier.</summary>
    public Guid PageActionId { get; set; }

    /// <summary>Gets or sets a value indicating whether this permission is granted.</summary>
    public bool IsGranted { get; set; } = true;

    /// <summary>Gets or sets the page navigation property.</summary>
    public Page? Page { get; set; }

    /// <summary>Gets or sets the page action navigation property.</summary>
    public PageAction? PageAction { get; set; }
}
