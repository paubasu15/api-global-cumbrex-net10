using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Platform;

/// <summary>Represents a tenant (organization) in the multi-tenant SaaS platform.</summary>
public class Tenant : BaseEntity
{
    /// <summary>Gets or sets the unique slug identifier for the tenant.</summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the tenant.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the tenant's primary email address.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Gets or sets an optional phone number for the tenant.</summary>
    public string? Phone { get; set; }

    /// <summary>Gets or sets the logo URL for the tenant.</summary>
    public string? LogoUrl { get; set; }

    /// <summary>Gets or sets a value indicating whether the tenant account is active.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the plan identifier associated with this tenant.</summary>
    public Guid? PlanId { get; set; }

    /// <summary>Gets or sets the plan associated with this tenant.</summary>
    public Plan? Plan { get; set; }

    /// <summary>Gets or sets the collection of custom domains for this tenant.</summary>
    public ICollection<TenantDomain> Domains { get; set; } = new List<TenantDomain>();
}
