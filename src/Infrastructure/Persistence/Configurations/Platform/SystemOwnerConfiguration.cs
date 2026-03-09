using CumbrexSaaS.Domain.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;

/// <summary>EF Core configuration for the <see cref="SystemOwner"/> entity.</summary>
public sealed class SystemOwnerConfiguration : BaseEntityConfiguration<SystemOwner>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<SystemOwner> builder)
    {
        builder.ToTable("TBL_SYSTEM_OWNERS");

        builder.Property(e => e.UserId)
            .HasColumnName("USER_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => e.UserId)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_SYSTEM_OWNERS_USER_ID");
    }
}
