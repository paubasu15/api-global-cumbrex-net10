namespace CumbrexSaaS.Domain.Common;

/// <summary>
/// Base class for tenant-scoped domain entities.
/// Extends <see cref="BaseEntity"/> with multi-tenancy support.
/// </summary>
public abstract class BaseTenantEntity : BaseEntity
{
    /// <summary>Gets or sets the tenant identifier this entity belongs to.</summary>
    public Guid TenantId { get; set; }
}
