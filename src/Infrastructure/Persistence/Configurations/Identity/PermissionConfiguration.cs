using CumbrexSaaS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="Permission"/> entity.</summary>
public sealed class PermissionConfiguration : BaseEntityConfiguration<Permission>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("TBL_PERMISSIONS");

        builder.Property(e => e.Code)
            .HasColumnName("CODE")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.Group)
            .HasColumnName("GROUP_NAME")
            .HasColumnType("NVARCHAR(100)");

        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_PERMISSIONS_CODE");
    }
}
