using CumbrexSaaS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="User"/> entity.</summary>
public sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TBL_USERS");

        builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasColumnType("NVARCHAR(256)")
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasColumnName("FIRST_NAME")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasColumnName("LAST_NAME")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Phone)
            .HasColumnName("PHONE")
            .HasColumnType("NVARCHAR(30)");

        builder.Property(e => e.AvatarUrl)
            .HasColumnName("AVATAR_URL")
            .HasColumnType("NVARCHAR(500)");

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(e => e.EmailVerifiedAt)
            .HasColumnName("EMAIL_VERIFIED_AT")
            .HasColumnType("DATETIME2(7)");

        builder.HasIndex(e => e.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_USERS_EMAIL");
    }
}
