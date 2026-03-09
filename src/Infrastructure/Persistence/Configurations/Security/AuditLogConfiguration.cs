using CumbrexSaaS.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;

/// <summary>EF Core configuration for the <see cref="AuditLog"/> entity.</summary>
public sealed class AuditLogConfiguration : BaseEntityConfiguration<AuditLog>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("TBL_AUDIT_LOGS");

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(e => e.EntityName)
            .HasColumnName("ENTITY_NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.EntityId)
            .HasColumnName("ENTITY_ID")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Action)
            .HasColumnName("ACTION")
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder.Property(e => e.OldValues)
            .HasColumnName("OLD_VALUES")
            .HasColumnType("NVARCHAR(MAX)");

        builder.Property(e => e.NewValues)
            .HasColumnName("NEW_VALUES")
            .HasColumnType("NVARCHAR(MAX)");

        builder.Property(e => e.IpAddress)
            .HasColumnName("IP_ADDRESS")
            .HasColumnType("NVARCHAR(45)");

        builder.HasIndex(e => new { e.TenantId, e.EntityName, e.EntityId })
            .HasDatabaseName("IX_TBL_AUDIT_LOGS_TENANT_ENTITY");

        builder.HasIndex(e => new { e.UserId, e.CreatedAt })
            .HasDatabaseName("IX_TBL_AUDIT_LOGS_USER_CREATED");
    }
}
