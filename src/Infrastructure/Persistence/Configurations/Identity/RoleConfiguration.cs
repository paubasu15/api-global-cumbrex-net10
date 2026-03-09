using CumbrexSaaS.Domain.Entities.Identity;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="Role"/> entity.</summary>
public sealed class RoleConfiguration : BaseTenantEntityConfiguration<Role>
{
    /// <summary>Initializes a new instance of <see cref="RoleConfiguration"/>.</summary>
    public RoleConfiguration(ITenantContext tenantContext) : base(tenantContext) { }

    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("TBL_ROLES");

        builder.Property(e => e.Code)
            .HasColumnName("CODE")
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasColumnType("NVARCHAR(500)");

        builder.Property(e => e.IsSystem)
            .HasColumnName("IS_SYSTEM")
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => new { e.TenantId, e.Code })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_ROLES_TENANT_CODE");
    }
}
