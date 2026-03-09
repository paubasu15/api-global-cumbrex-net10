using CumbrexSaaS.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Authorization;

/// <summary>EF Core configuration for the <see cref="Page"/> entity.</summary>
public sealed class PageConfiguration : BaseEntityConfiguration<Page>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("TBL_PAGES");

        builder.Property(e => e.Code)
            .HasColumnName("CODE")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.ParentId)
            .HasColumnName("PARENT_ID")
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(e => e.Order)
            .HasColumnName("DISPLAY_ORDER")
            .HasColumnType("INT")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(e => e.Icon)
            .HasColumnName("ICON")
            .HasColumnType("NVARCHAR(100)");

        builder.Property(e => e.Route)
            .HasColumnName("ROUTE")
            .HasColumnType("NVARCHAR(500)");

        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_PAGES_CODE");
    }
}
