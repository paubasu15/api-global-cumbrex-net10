using CumbrexSaaS.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;

/// <summary>EF Core configuration for the <see cref="UserSession"/> entity.</summary>
public sealed class UserSessionConfiguration : BaseEntityConfiguration<UserSession>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("TBL_USER_SESSIONS");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.IpAddress)
            .HasColumnName("IP_ADDRESS")
            .HasColumnType("NVARCHAR(45)")
            .IsRequired();

        builder.Property(e => e.UserAgent)
            .HasColumnName("USER_AGENT")
            .HasColumnType("NVARCHAR(500)");

        builder.Property(e => e.DeviceType)
            .HasColumnName("DEVICE_TYPE")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(e => e.LastActiveAt)
            .HasColumnName("LAST_ACTIVE_AT")
            .HasColumnType("DATETIME2(7)")
            .IsRequired();

        builder.Property(e => e.ExpiresAt)
            .HasColumnName("EXPIRES_AT")
            .HasColumnType("DATETIME2(7)")
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => new { e.UserId, e.TenantId, e.IsActive })
            .HasDatabaseName("IX_TBL_USER_SESSIONS_USER_TENANT_ACTIVE");
    }
}
