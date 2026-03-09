namespace CumbrexSaaS.Infrastructure.MultiTenancy;

/// <summary>
/// Provides access to the current tenant identifier for the request scope.
/// </summary>
public interface ITenantContext
{
    /// <summary>Gets the current tenant's identifier. May be <see cref="Guid.Empty"/> for system-level operations.</summary>
    Guid TenantId { get; }

    /// <summary>Gets a value indicating whether a tenant has been resolved for this request.</summary>
    bool HasTenant { get; }

    /// <summary>Sets the current tenant identifier. Called by tenant resolution middleware.</summary>
    void SetTenant(Guid tenantId);
}
