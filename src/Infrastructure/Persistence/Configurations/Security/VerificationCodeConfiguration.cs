using CumbrexSaaS.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;

/// <summary>EF Core configuration for the <see cref="VerificationCode"/> entity.</summary>
public sealed class VerificationCodeConfiguration : BaseEntityConfiguration<VerificationCode>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("TBL_VERIFICATION_CODES");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.CodeHash)
            .HasColumnName("CODE_HASH")
            .HasColumnType("NVARCHAR(128)")
            .IsRequired();

        builder.Property(e => e.Channel)
            .HasColumnName("CHANNEL")
            .HasColumnType("INT")
            .IsRequired();

        builder.Property(e => e.Destination)
            .HasColumnName("DESTINATION")
            .HasColumnType("NVARCHAR(256)")
            .IsRequired();

        builder.Property(e => e.ExpiresAt)
            .HasColumnName("EXPIRES_AT")
            .HasColumnType("DATETIME2(7)")
            .IsRequired();

        builder.Property(e => e.IsUsed)
            .HasColumnName("IS_USED")
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.UsedAt)
            .HasColumnName("USED_AT")
            .HasColumnType("DATETIME2(7)");

        builder.Property(e => e.AttemptCount)
            .HasColumnName("ATTEMPT_COUNT")
            .HasColumnType("INT")
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasIndex(e => new { e.UserId, e.Channel, e.IsUsed, e.ExpiresAt })
            .HasDatabaseName("IX_TBL_VERIFICATION_CODES_USER_CHANNEL_ACTIVE");
    }
}
