using CumbrexSaaS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="RolePermission"/> entity.</summary>
public sealed class RolePermissionConfiguration : BaseEntityConfiguration<RolePermission>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("TBL_ROLE_PERMISSIONS");

        builder.Property(e => e.RoleId)
            .HasColumnName("ROLE_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.PermissionId)
            .HasColumnName("PERMISSION_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.HasIndex(e => new { e.RoleId, e.PermissionId })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_ROLE_PERMISSIONS_ROLE_PERMISSION");

        builder.HasOne(e => e.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(e => e.RoleId)
            .HasConstraintName("FK_TBL_ROLE_PERMISSIONS_ROLE_ID")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(e => e.PermissionId)
            .HasConstraintName("FK_TBL_ROLE_PERMISSIONS_PERMISSION_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
