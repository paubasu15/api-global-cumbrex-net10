using CumbrexSaaS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="UserTenant"/> entity.</summary>
public sealed class UserTenantConfiguration : BaseEntityConfiguration<UserTenant>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<UserTenant> builder)
    {
        builder.ToTable("TBL_USER_TENANTS");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => new { e.UserId, e.TenantId })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_USER_TENANTS_USER_TENANT");

        builder.HasIndex(e => e.TenantId)
            .HasDatabaseName("IX_TBL_USER_TENANTS_TENANT_ID");

        builder.HasOne(e => e.User)
            .WithMany(u => u.UserTenants)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FK_TBL_USER_TENANTS_USER_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
