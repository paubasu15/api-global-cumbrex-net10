using CumbrexSaaS.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;

/// <summary>EF Core configuration for the <see cref="RefreshToken"/> entity.</summary>
public sealed class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("TBL_REFRESH_TOKENS");

        builder.Property(e => e.UserSessionId)
            .HasColumnName("USER_SESSION_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.TokenHash)
            .HasColumnName("TOKEN_HASH")
            .HasColumnType("NVARCHAR(128)")
            .IsRequired();

        builder.Property(e => e.ExpiresAt)
            .HasColumnName("EXPIRES_AT")
            .HasColumnType("DATETIME2(7)")
            .IsRequired();

        builder.Property(e => e.IsRevoked)
            .HasColumnName("IS_REVOKED")
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.RevokedAt)
            .HasColumnName("REVOKED_AT")
            .HasColumnType("DATETIME2(7)");

        builder.HasIndex(e => e.TokenHash)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_REFRESH_TOKENS_TOKEN_HASH");

        builder.HasIndex(e => e.UserSessionId)
            .HasDatabaseName("IX_TBL_REFRESH_TOKENS_USER_SESSION_ID");

        builder.HasOne(e => e.UserSession)
            .WithMany(s => s.RefreshTokens)
            .HasForeignKey(e => e.UserSessionId)
            .HasConstraintName("FK_TBL_REFRESH_TOKENS_USER_SESSION_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
