using CumbrexSaaS.Domain.Entities.Authorization;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Authorization;

/// <summary>EF Core configuration for the <see cref="RolePagePermission"/> entity.</summary>
public sealed class RolePagePermissionConfiguration : BaseTenantEntityConfiguration<RolePagePermission>
{
    /// <summary>Initializes a new instance of <see cref="RolePagePermissionConfiguration"/>.</summary>
    public RolePagePermissionConfiguration(ITenantContext tenantContext) : base(tenantContext) { }

    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<RolePagePermission> builder)
    {
        builder.ToTable("TBL_ROLE_PAGE_PERMISSIONS");

        builder.Property(e => e.RoleId)
            .HasColumnName("ROLE_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.PageId)
            .HasColumnName("PAGE_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.PageActionId)
            .HasColumnName("PAGE_ACTION_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.IsGranted)
            .HasColumnName("IS_GRANTED")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => new { e.TenantId, e.RoleId, e.PageId, e.PageActionId })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_ROLE_PAGE_PERMISSIONS_ROLE_PAGE_ACTION");

        builder.HasOne(e => e.Page)
            .WithMany(p => p.RolePagePermissions)
            .HasForeignKey(e => e.PageId)
            .HasConstraintName("FK_TBL_ROLE_PAGE_PERMISSIONS_PAGE_ID")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PageAction)
            .WithMany(pa => pa.RolePagePermissions)
            .HasForeignKey(e => e.PageActionId)
            .HasConstraintName("FK_TBL_ROLE_PAGE_PERMISSIONS_PAGE_ACTION_ID")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
