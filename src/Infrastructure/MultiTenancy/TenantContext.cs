namespace CumbrexSaaS.Infrastructure.MultiTenancy;

/// <summary>
/// Scoped implementation of <see cref="ITenantContext"/>.
/// The tenant is resolved once per HTTP request by <see cref="TenantResolutionMiddleware"/>.
/// </summary>
public sealed class TenantContext : ITenantContext
{
    private Guid _tenantId = Guid.Empty;

    /// <inheritdoc />
    public Guid TenantId => _tenantId;

    /// <inheritdoc />
    public bool HasTenant => _tenantId != Guid.Empty;

    /// <inheritdoc />
    public void SetTenant(Guid tenantId) => _tenantId = tenantId;
}
