using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Platform;

/// <summary>Represents a custom domain mapping for a tenant.</summary>
public class TenantDomain : BaseEntity
{
    /// <summary>Gets or sets the tenant identifier this domain belongs to.</summary>
    public Guid TenantId { get; set; }

    /// <summary>Gets or sets the hostname (e.g., app.acme.com).</summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>Gets or sets a value indicating whether this is the primary domain.</summary>
    public bool IsPrimary { get; set; }

    /// <summary>Gets or sets the tenant navigation property.</summary>
    public Tenant? Tenant { get; set; }
}
