using CumbrexSaaS.Domain.Entities.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Authorization;

/// <summary>EF Core configuration for the <see cref="PageAction"/> entity.</summary>
public sealed class PageActionConfiguration : BaseEntityConfiguration<PageAction>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<PageAction> builder)
    {
        builder.ToTable("TBL_PAGE_ACTIONS");

        builder.Property(e => e.PageId)
            .HasColumnName("PAGE_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.Code)
            .HasColumnName("CODE")
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.HasIndex(e => new { e.PageId, e.Code })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_PAGE_ACTIONS_PAGE_CODE");

        builder.HasOne(e => e.Page)
            .WithMany(p => p.Actions)
            .HasForeignKey(e => e.PageId)
            .HasConstraintName("FK_TBL_PAGE_ACTIONS_PAGE_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
