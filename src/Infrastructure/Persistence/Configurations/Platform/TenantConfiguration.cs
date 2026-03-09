using CumbrexSaaS.Domain.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;

/// <summary>EF Core configuration for the <see cref="Tenant"/> entity.</summary>
public sealed class TenantConfiguration : BaseEntityConfiguration<Tenant>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("TBL_TENANTS");

        builder.Property(e => e.Slug)
            .HasColumnName("SLUG")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasColumnType("NVARCHAR(256)")
            .IsRequired();

        builder.Property(e => e.Phone)
            .HasColumnName("PHONE")
            .HasColumnType("NVARCHAR(30)");

        builder.Property(e => e.LogoUrl)
            .HasColumnName("LOGO_URL")
            .HasColumnType("NVARCHAR(500)");

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(e => e.PlanId)
            .HasColumnName("PLAN_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.HasIndex(e => e.Slug)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_TENANTS_SLUG");

        builder.HasIndex(e => e.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_TENANTS_EMAIL");

        builder.HasOne(e => e.Plan)
            .WithMany(p => p.Tenants)
            .HasForeignKey(e => e.PlanId)
            .HasConstraintName("FK_TBL_TENANTS_PLAN_ID")
            .OnDelete(DeleteBehavior.SetNull);
    }
}
