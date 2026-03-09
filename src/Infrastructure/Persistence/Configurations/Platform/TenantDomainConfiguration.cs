using CumbrexSaaS.Domain.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;

/// <summary>EF Core configuration for the <see cref="TenantDomain"/> entity.</summary>
public sealed class TenantDomainConfiguration : BaseEntityConfiguration<TenantDomain>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<TenantDomain> builder)
    {
        builder.ToTable("TBL_TENANT_DOMAINS");

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.Host)
            .HasColumnName("HOST")
            .HasColumnType("NVARCHAR(253)")
            .IsRequired();

        builder.Property(e => e.IsPrimary)
            .HasColumnName("IS_PRIMARY")
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasIndex(e => e.Host)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_TENANT_DOMAINS_HOST");

        builder.HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_TBL_TENANT_DOMAINS_TENANT_ID");

        builder.HasOne(e => e.Tenant)
            .WithMany(t => t.Domains)
            .HasForeignKey(e => e.TenantId)
            .HasConstraintName("FK_TBL_TENANT_DOMAINS_TENANT_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
