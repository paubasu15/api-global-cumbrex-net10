using CumbrexSaaS.Domain.Common;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations;

/// <summary>
/// Base EF Core configuration for all <see cref="BaseTenantEntity"/> types.
/// Extends <see cref="BaseEntityConfiguration{TEntity}"/> with a TENANT_ID column,
/// a foreign key to TBL_TENANTS and a tenant-scoped global query filter.
/// </summary>
public abstract class BaseTenantEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
    where TEntity : BaseTenantEntity
{
    private readonly ITenantContext _tenantContext;

    /// <summary>Initializes a new instance of <see cref="BaseTenantEntityConfiguration{TEntity}"/>.</summary>
    protected BaseTenantEntityConfiguration(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        // Combined global query filter: soft-delete + tenant isolation
        builder.HasQueryFilter(e => !e.IsDeleted && e.TenantId == _tenantContext.TenantId);
    }
}
