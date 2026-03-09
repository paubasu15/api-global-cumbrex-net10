using CumbrexSaaS.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;

/// <summary>EF Core configuration for the <see cref="LoginAttempt"/> entity.</summary>
public sealed class LoginAttemptConfiguration : BaseEntityConfiguration<LoginAttempt>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<LoginAttempt> builder)
    {
        builder.ToTable("TBL_LOGIN_ATTEMPTS");

        builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasColumnType("NVARCHAR(256)")
            .IsRequired();

        builder.Property(e => e.IpAddress)
            .HasColumnName("IP_ADDRESS")
            .HasColumnType("NVARCHAR(45)")
            .IsRequired();

        builder.Property(e => e.UserAgent)
            .HasColumnName("USER_AGENT")
            .HasColumnType("NVARCHAR(500)");

        builder.Property(e => e.IsSuccess)
            .HasColumnName("IS_SUCCESS")
            .HasColumnType("BIT")
            .IsRequired();

        builder.Property(e => e.FailureReason)
            .HasColumnName("FAILURE_REASON")
            .HasColumnType("INT");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(e => e.TenantId)
            .HasColumnName("TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.HasIndex(e => new { e.Email, e.CreatedAt })
            .HasDatabaseName("IX_TBL_LOGIN_ATTEMPTS_EMAIL_CREATED");

        builder.HasIndex(e => new { e.IpAddress, e.CreatedAt })
            .HasDatabaseName("IX_TBL_LOGIN_ATTEMPTS_IP_CREATED");
    }
}
